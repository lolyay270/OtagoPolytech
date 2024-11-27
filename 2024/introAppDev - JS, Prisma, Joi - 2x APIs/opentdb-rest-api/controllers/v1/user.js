/**
 * @file Manages functions for Users
 * @author Jenna Boyes
 */

import STATUS_CODES from '../../utils/statusCode.js';
import Repo from '../../repositories/user.js';

/**
 * @description Get all Users
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const getAllUsers = async (req, res) => {
  try {
    const users = await Repo.findAll();

    if (users.length === 0) {
      return res.status(STATUS_CODES.NOT_FOUND).json({
        msg: `No Users found`,
      });
    }

    return res.json({ data: users });
  } catch (err) {
    return res.status(STATUS_CODES.SERVER_ERROR).json({
      msg: err.message,
    });
  }
};

export { getAllUsers };
