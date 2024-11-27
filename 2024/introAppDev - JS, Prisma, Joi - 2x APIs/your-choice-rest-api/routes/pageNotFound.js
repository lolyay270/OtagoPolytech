/**
 * @file Invalid directory is loaded
 * @author Jenna Boyes
 */

import STATUS_CODES from '../utils/statusCode.js';

/**
 * @description Runs when an invalid directory is loaded
 * @param {object} req - The request object
 * @param {object} res - 
 * @returns 
 */
const getPageNotFound = (req, res) => {
  return res.status(STATUS_CODES.NOT_FOUND).json({
    msg: 'Page not found',
  });
};

export { getPageNotFound };
