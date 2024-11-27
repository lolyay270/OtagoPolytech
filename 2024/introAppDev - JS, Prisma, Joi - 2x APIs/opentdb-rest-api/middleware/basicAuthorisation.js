/**
 * @file Ensures User has BASIC Role
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

const basicAuthorisation = async (req, res, next) => {
  try {
    const { id } = req.user;

    const user = await prismaClient.user.findUnique({ where: { id: id } });

    // Check if the user is an admin
    if (user.role !== 'BASIC') {
      return res.status(403).json({
        msg: 'Not authorized to access this route',
      });
    }

    next();
  } catch (err) {
    return res.status(500).json({
      msg: err.message,
    });
  }
};

export default basicAuthorisation;
