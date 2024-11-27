/**
 * @file Manages functions for Scores
 * @author Jenna Boyes
 */

import STATUS_CODES from '../../utils/statusCode.js';
import Repo from '../../repositories/userQuizScore.js';

/**
 * @description Get all Scores
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const getAllScores = async (req, res) => {
  try {
    const scores = await Repo.findAll();

    if (scores.length === 0) {
      return res.status(STATUS_CODES.NOT_FOUND).json({
        msg: `No Scores found`,
      });
    }

    return res.json({ data: scores });
  } catch (err) {
    return res.status(STATUS_CODES.SERVER_ERROR).json({
      msg: err.message,
    });
  }
};

export { getAllScores };
