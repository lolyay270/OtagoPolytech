/**
 * @file Manages functions for Authentication
 * @author Jenna Boyes
 */

import bcryptjs from 'bcryptjs';
import jwt from 'jsonwebtoken';
import { prismaClient } from '../../prisma/prisma.js';

/**
 * @description Register an Authenticated User
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns The response object
 */
const register = async (req, res) => {
  try {
    const { firstName, lastName, emailAddress, password } = req.body;

    if (role === 'ADMIN_USER') {
      return res.status(403).json({ msg: 'User cannot register as an admin' });
    }

    let user = await prismaClient.user.findUnique({ where: { emailAddress } });

    if (user) return res.status(409).json({ msg: 'User already exists' });

    /**
     * A salt is random bits added to a password before it is hashed. Salts
     * create unique passwords even if two users have the same passwords
     */
    const salt = await bcryptjs.genSalt();

    /**
     * Generate a hash for a given string. The first argument
     * is a string to be hashed, i.e., Pazzw0rd123 and the second
     * argument is a salt, i.e., E1F53135E559C253
     */
    const hashedPassword = await bcryptjs.hash(password, salt);

    user = await prismaClient.user.create({
      data: {
        firstName,
        lastName,
        emailAddress,
        password: hashedPassword,
        role: 'BASIC',
      },
    });

    // Delete the password property from the user object
    delete user.password;

    return res.status(201).json({
      msg: 'User successfully registered',
      data: user,
    });
  } catch (err) {
    return res.status(500).json({
      msg: err.message,
    });
  }
};

/**
 * @description Login an Authenticated User
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns The response object
 */
const login = async (req, res) => {
  const MAX_LOGIN_ATTEMPTS = 5;
  const LOCK_TIME_MS = 5 * 60 * 1000; // 5 minutes

  try {
    const { emailAddress, password } = req.body;

    const user = await prismaClient.user.findUnique({
      where: { emailAddress },
    });

    if (!user) return res.status(401).json({ msg: 'Invalid email address' });

    if (
      user.loginAttempts >= MAX_LOGIN_ATTEMPTS &&
      user.lastLoginAttempt >= Date.now() - LOCK_TIME_MS
    ) {
      return res.status(401).json({
        msg: 'Maximum login attempts reached. Please try again later',
      });
    }

    /**
     * Compare the given string, i.e., Pazzw0rd123, with the given
     * hash, i.e., user's hashed password
     */
    const isPasswordCorrect = await bcryptjs.compare(password, user.password);

    if (!isPasswordCorrect) {
      await prismaClient.user.update({
        where: { emailAddress },
        data: {
          loginAttempts: user.loginAttempts + 1,
          lastLoginAttempt: new Date(),
        },
      });

      return res.status(401).json({ msg: 'Invalid password' });
    }

    const { JWT_SECRET, JWT_LIFETIME } = process.env;

    /**
     * Return a JWT. The first argument is the payload, i.e., an object containing
     * the authenticated user's id and name, the second argument is the secret
     * or public/private key, and the third argument is the lifetime of the JWT
     */
    const token = jwt.sign(
      {
        id: user.id,
        name: user.name,
      },
      JWT_SECRET,
      { expiresIn: JWT_LIFETIME },
    );

    await prismaClient.user.update({
      where: { emailAddress },
      data: {
        loginAttempts: 0,
        lastLoginAttempt: null,
      },
    });

    return res.status(200).json({
      msg: 'User successfully logged in',
      user: {
        id: user.id,
        firstName: user.firstName,
        lastName: user.lastName,
        emailAddress: user.emailAddress,
      },
      token: token,
    });
  } catch (err) {
    return res.status(500).json({
      msg: err.message,
    });
  }
};

export { register, login };