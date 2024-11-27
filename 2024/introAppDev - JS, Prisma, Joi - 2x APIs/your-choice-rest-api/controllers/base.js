/**
 * @file Basic version of controller, that all other controllers call
 * @author Jenna Boyes
 */

import STATUS_CODES from '../utils/statusCode.js';
import baseRepo from '../repositories/base.js';
import { Prisma } from '../prisma/prisma.js';
/**
 * @description This function creates a singular resource of model type
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @param {string[]} [uniqueFields] - An array of unique field names in plain text
 * @returns {object} The response object
 */
const createResource = async (req, res, model, uniqueFields) => {
  try {
    await baseRepo.create(req.body, model);

    const newResources = await baseRepo.findAll(model);

    return res.status(STATUS_CODES.CREATED).json({
      msg: `${capitaliseFirstLetter(model)} successfully created`,
      data: newResources,
    });
  } catch (err) {
    if (err instanceof Prisma.PrismaClientKnownRequestError) {
      if (err.code === 'P2002') {
        return sameFieldExists(res, model, uniqueFields);
      }
    } else {
      return errorResponse(res, err);
    }
  }
};

/**
 * @description This function gets all resources for a model
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @param {object} baseQuery - The query options object
 * @param {object} [baseQuery.include] - The relation to include, eg. { home: 'all', school: ['name', 'address'], }
 * @param {object} [baseQuery.filters] - The fields to filter, eg. { name: ['string', 'contains'], age: ['number', 'equals'], }
 * @param {object} [baseQuery.defaultSort] - The sorting of fields, eg. { field: 'firstName', order 'asc', }
 * @returns {object} The response object
 */
const getResources = async (req, res, model, baseQuery) => {
  try {
    const resources = await baseRepo.findAll(model, req.query, baseQuery);

    if (resources.length === 0) {
      return res.status(STATUS_CODES.NOT_FOUND).json({
        msg: `No ${model}s found`,
      });
    }

    return res.json({ data: resources });
  } catch (err) {
    return errorResponse(res, err);
  }
};

/**
 * @description This function gets a singular resource of model type, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @returns {object} The response object
 */
const getResource = async (req, res, model) => {
  try {
    const resource = await baseRepo.findById(req.params.id, model);

    if (!resource) {
      return noResource(req, res, model);
    }

    return res.json({
      data: resource,
    });
  } catch (err) {
    return errorResponse(res, err);
  }
};

/**
 * @description This function updates a singular resource of model type, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @param {string[]} [uniqueFields] - An array of unique field names in plain text
 * @returns {object} The response object
 */
const updateResource = async (req, res, model, uniqueFields) => {
  try {
    let resource = await baseRepo.findById(req.params.id, model);

    if (!resource) {
      return noResource(req, res, model);
    }

    resource = await baseRepo.update(req.params.id, req.body, model);

    return res.json({
      msg: `${capitaliseFirstLetter(model)} with the id: ${req.params.id} successfully updated`,
      data: resource,
    });
  } catch (err) {
    if (err instanceof Prisma.PrismaClientKnownRequestError) {
      if (err.code === 'P2002') {
        return sameFieldExists(res, model, uniqueFields);
      }
    } else {
      return errorResponse(res, err);
    }
  }
};

/**
 * @description This function deletes a singular resource of model type, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @returns {object} The response object
 */
const deleteResource = async (req, res, model) => {
  try {
    const resource = await baseRepo.findById(req.params.id, model);

    if (!resource) {
      return noResource(req, res, model);
    }

    await baseRepo.delete(req.params.id, model);

    return res.json({
      msg: `${capitaliseFirstLetter(model)} with the id: ${req.params.id} successfully deleted`,
    });
  } catch (err) {
    return errorResponse(res, err);
  }
};

//--------------- Utility Methods ---------------\\
/**
 * @description - This function changes a string's first char to uppercase
 * @param {object} string - The text you want to adjust
 * @returns {object} - The adjusted string
 */
const capitaliseFirstLetter = (string) =>
  string.charAt(0).toUpperCase() + string.slice(1);

/**
 * @description - This function gives the caught error messages
 * @param {object} res - The response object
 * @param {object} err - The catched error
 * @returns {object} - The response object
 */
const errorResponse = (res, err) => {
  return res.status(STATUS_CODES.SERVER_ERROR).json({
    msg: err.message,
  });
};

/**
 * @description This function gives the "no objects with the id [id] found" error message
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @returns {object} The response object
 */
const noResource = (req, res, model) => {
  return res.status(STATUS_CODES.NOT_FOUND).json({
    msg: `No ${model} with the id: ${req.params.id} found`,
  });
};

/**
 * @description This function makes a sentence from uniqueFields
 * @param {object} res - The response object
 * @param {string} model - The data's model
 * @param {string[]} uniqueFields - An array of unique field names in plain text
 * @returns {object} error code with description
 */
const sameFieldExists = (res, model, uniqueFields) => {
  let text = `${capitaliseFirstLetter(model)} with the same `;

  if (!uniqueFields)
    return errorResponse(res, {
      message: `${STATUS_CODES.VALIDATE_ERROR} error encounted with no given unique field names`,
    });
  else if (uniqueFields.length === 1) text += uniqueFields;
  else if (uniqueFields.length === 2) {
    text += `${uniqueFields[0]} or ${uniqueFields[1]}`;
  } else {
    uniqueFields.forEach((field) => {
      if (field === uniqueFields.length - 2) text += `${field} or `;
      else if (field === uniqueFields.length - 1) text += field;
      else text += `${field}, `;
    });
  }

  text += ` already exists`;

  return res.status(STATUS_CODES.VALIDATE_ERROR).json({
    message: text,
  });
};

export {
  createResource,
  getResources,
  getResource,
  updateResource,
  deleteResource,
};
