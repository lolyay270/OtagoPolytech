/**
 * @file Manages functions for Categories
 * @author Jenna Boyes
 */

import STATUS_CODES from '../../utils/statusCode.js';
import { categoryNames, categoryIds } from '../../utils/categories.js';

/**
 * @description This function converts category ID into name
 * @param {Object} res - The response object
 * @param {Int} id - The name of the category
 * @returns The related name or error message
 */
const convertIdToName = async (res, id) => {
  const index = categoryIds.indexOf(id);

  if (index === -1) {
    //no id matches
    return res.status(STATUS_CODES.NOT_FOUND).json({
      msg: 'Invalid Category id, must be from https://opentdb.com/api_category.php',
    });
  } else {
    return categoryNames[index];
  }
};

export { convertIdToName };
