/**
 * @file Validate data going thru API to DB
 * @author Jenna Boyes
 */

import Joi from 'joi';
import STATUS_CODES from '../utils/statusCode.js';

const temperatureEnums = ['TROPICAL', 'COLD'];

//----------------TANK----------------\\
const validatePostTank = (req, res, next) => {
  const tankSchema = Joi.object({
    name: Joi.string().max(20).required().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
      'any.required': 'name is required',
    }),
    totalFishSize: Joi.number().integer().min(1).max(1120).required().messages({
      'number.base': 'totalFishSize should be a number',
      'number.integer': 'totalFishSize should be a whole number',
      'number.min': 'totalFishSize should be equal to or greater than {#limit}',
      'number.max': 'totalFistSize should be equal to or less than {#limit}',
      'any.required': 'totalFishSize is required',
    }),
    floorArea: Joi.number().integer().min(1).max(800).required().messages({
      'number.base': 'floorArea should be a number',
      'number.integer': 'floorArea should be a whole number',
      'number.min': 'floorArea should be equal to or greater than {#limit}',
      'number.max': 'floorArea should be equal to or less than {#limit}',
      'any.required': 'floorArea is required',
    }),
    rounded: Joi.bool().required().messages({
      'boolean.base': 'rounded should be true or false',
      'any.required': 'rounded is required',
    }),
    levels: Joi.array().items(
      Joi.string().uuid({ version: ['uuidv4'] }), //requires at least 1 uuid, can have more
    ).min(1).unique().required().messages({
      //checks for array
      'array.base': 'levels should be an array',
      'array.min': 'levels should not be empty',
      'array.unique': 'Id in levels should not be duplicate',
      'any.required': 'levels is required',
      //checks for uuid inside array
      'string.base': 'Id in levels should be a string',
      'string.empty': 'Id in levels cannot be empty',
      'string.guid': 'Id in levels should be of uuid version 4 format', //prisma generates uuidv4
      'string.required': 'Id in levels is required',
    }),
  });
  const error = errorCheck(req.body, res, tankSchema);
  if (error) return error;
  next();
};

const validatePutTank = (req, res, next) => {
  const tankSchema = Joi.object({
    name: Joi.string().max(20).optional().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
      'any.required': 'name is required',
    }),
    totalFishSize: Joi.number().integer().min(1).max(1120).optional().messages({
      'number.base': 'totalFishSize should be a number',
      'number.integer': 'totalFishSize should be a whole number',
      'number.min': 'totalFishSize should be equal to or greater than {#limit}',
      'number.max': 'totalFistSize should be equal to or less than {#limit}',
      'any.required': 'totalFishSize is required',
    }),
    floorArea: Joi.number().integer().min(1).max(800).optional().messages({
      'number.base': 'floorArea should be a number',
      'number.integer': 'floorArea should be a whole number',
      'number.min': 'floorArea should be equal to or greater than {#limit}',
      'number.max': 'floorArea should be equal to or less than {#limit}',
      'any.required': 'floorArea is required',
    }),
    rounded: Joi.bool().optional().messages({
      'boolean.base': 'rounded should be true or false',
      'any.required': 'rounded is required',
    }),
  }).min(1).messages({
    'object.min': 'To update, enter at least 1 field to change',
  });
  const error = errorCheck(req.body, res, tankSchema);
  if (error) return error;
  next();
};

//----------------FISH----------------\\
const validatePostFish = (req, res, next) => {
  const fishSchema = Joi.object({
    scientificName: Joi.string().min(3).max(75).messages({
      'string.base': 'scientificName should be a string',
      'string.empty': 'scientificName cannot be empty',
      'string.min': 'scientificName should have a minimum length of {#limit}',
      'string.max': 'scientificName should have a maximum length of {#limit}',
      'any.required': 'scientificName is required',
    }),
    commonName: Joi.string().max(75).messages({
      'string.base': 'commonName should be a string',
      'string.empty': 'scientificName cannot be empty',
      'string.max': 'commonName should have a maximum length of {#limit}',
    }),
    maxSize: Joi.number().integer().min(1).max(1000).required().messages({
      'number.base': 'maxSize should be a number',
      'number.integer': 'maxSize should be a whole number',
      'number.min': 'maxSize should be equal to or greater than {#limit}',
      'number.max': 'maxSize should be equal to or less than {#limit}',
      'any.required': 'maxSize is required',
    }),
    //prettier-ignore
    temperature: Joi.string().valid(...Object.values(temperatureEnums)).required().messages({
      'string.base': 'temperature should be a string', //---------TEST FOR TYPE STRING DOESNT FIRE-----------\\
      'any.only': 'temperature should be any of {#valids}',
      'any.required': 'temperature is required',
    }),
    // prettier-ignore
    tankId: Joi.string().uuid({ version: ['uuidv4'] }).required().messages({
      'string.base': 'tankId should be a string',
      'string.empty': 'tankId cannot be empty',
      'string.guid': 'tankId should be of uuid version 4 format', //prisma generates uuidv4
      'any.required': 'tankId is required',
      }),
  });
  const error = errorCheck(req.body, res, fishSchema);
  if (error) return error;
  next();
};

const validatePutFish = (req, res, next) => {
  const fishSchema = Joi.object({
    scientificName: Joi.string().min(3).max(75).optional().messages({
      'string.base': 'scientificName should be a string',
      'string.empty': 'scientificName cannot be empty',
      'string.min': 'scientificName should have a minimum length of {#limit}',
      'string.max': 'scientificName should have a maximum length of {#limit}',
      'any.required': 'scientificName is required',
    }),
    commonName: Joi.string().max(75).optional().messages({
      'string.base': 'commonName should be a string',
      'string.empty': 'scientificName cannot be empty',
      'string.max': 'commonName should have a maximum length of {#limit}',
    }),
    maxSize: Joi.number().integer().min(1).max(1000).optional().messages({
      'number.base': 'maxSize should be a number',
      'number.integer': 'maxSize should be a whole number',
      'number.min': 'maxSize should be equal to or greater than {#limit}',
      'number.max': 'maxSize should be equal to or less than {#limit}',
      'any.required': 'maxSize is required',
    }),
    //prettier-ignore
    temperature: Joi.string().valid(...Object.values(temperatureEnums)).optional().messages({
      'string.base': 'temperature should be a string', //---------TEST FOR TYPE STRING DOESNT FIRE-----------\\
      'any.only': 'temperature should be any of {#valids}',
      'any.required': 'temperature is required',
    }),
    // prettier-ignore
    tankId: Joi.string().uuid({ version: ['uuidv4'] }).optional().messages({
      'string.base': 'tankId should be a string',
      'string.empty': 'tankId cannot be empty',
      'string.guid': 'tankId should be of uuid version 4 format', //prisma generates uuidv4
      'any.required': 'tankId is required',
      }),
  }).min(1).messages({
    'object.min': 'To update, enter at least 1 field to change',
  });
  const error = errorCheck(req.body, res, fishSchema);
  if (error) return error;
  next();
};

//----------------DECOR----------------\\
const validatePostDecor = (req, res, next) => {
  const decorSchema = Joi.object({
    name: Joi.string().max(20).required().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
      'any.required': 'name is required',
    }),
    plantAmount: Joi.number().integer().min(0).max(10).required().messages({
      'number.base': 'plantAmount should be a number',
      'number.integer': 'plantAmount should be a whole number',
      'number.min': 'plantAmount should be equal to or greater than {#limit}',
      'number.max': 'plantAmount should be equal to or less than {#limit}',
      'any.required': 'plantAmount is required',
    }),
    rockAmount: Joi.number().integer().min(0).max(10).required().messages({
      'number.base': 'rockAmount should be a number',
      'number.integer': 'rockAmount should be a whole number',
      'number.min': 'rockAmount should be equal to or greater than {#limit}',
      'number.max': 'rockAmount should be equal to or less than {#limit}',
      'any.required': 'rockAmount is required',
    }),
    caveAmount: Joi.number().integer().min(0).max(15).required().messages({
      'number.base': 'caveAmount should be a number',
      'number.integer': 'caveAmount should be a whole number',
      'number.min': 'caveAmount should be equal to or greater than {#limit}',
      'number.max': 'caveAmount should be equal to or less than {#limit}',
      'any.required': 'caveAmount is required',
    }),
    floorArea: Joi.number().integer().min(1).max(4).required().messages({
      'number.base': 'floorArea should be a number',
      'number.integer': 'floorArea should be a whole number',
      'number.min': 'floorArea should be equal to or greater than {#limit}',
      'number.max': 'floorArea should be equal to or less than {#limit}',
      'any.required': 'floorArea is required',
    }),
    //prettier-ignore
    tankId: Joi.string().uuid({ version: ['uuidv4'] }).required().messages({
      'string.base': 'tankId should be a string',
      'string.empty': 'tankId cannot be empty',
      'string.guid': 'tankId should be of uuid version 4 format', //prisma generates uuidv4
      'any.required': 'tankId is required',
    }),
  });
  const error = errorCheck(req.body, res, decorSchema);
  if (error) return error;
  next();
};

const validatePutDecor = (req, res, next) => {
  const decorSchema = Joi.object({
    name: Joi.string().max(20).optional().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
    }),
    plantAmount: Joi.number().integer().min(0).max(10).optional().messages({
      'number.base': 'plantAmount should be a number',
      'number.integer': 'plantAmount should be a whole number',
      'number.min': 'plantAmount should be equal to or greater than {#limit}',
      'number.max': 'plantAmount should be equal to or less than {#limit}',
    }),
    rockAmount: Joi.number().integer().min(0).max(10).optional().messages({
      'number.base': 'rockAmount should be a number',
      'number.integer': 'rockAmount should be a whole number',
      'number.min': 'rockAmount should be equal to or greater than {#limit}',
      'number.max': 'rockAmount should be equal to or less than {#limit}',
    }),
    caveAmount: Joi.number().integer().min(0).max(15).optional().messages({
      'number.base': 'caveAmount should be a number',
      'number.integer': 'caveAmount should be a whole number',
      'number.min': 'caveAmount should be equal to or greater than {#limit}',
      'number.max': 'caveAmount should be equal to or less than {#limit}',
    }),
    floorArea: Joi.number().integer().min(1).max(4).optional().messages({
      'number.base': 'floorArea should be a number',
      'number.integer': 'floorArea should be a whole number',
      'number.min': 'floorArea should be equal to or greater than {#limit}',
      'number.max': 'floorArea should be equal to or less than {#limit}',
    }),
    //prettier-ignore
    tankId: Joi.string().uuid({ version: ['uuidv4'] }).optional().messages({
    'string.base': 'tankId should be a string',
    'string.empty': 'tankId cannot be empty',
    'string.guid': 'tankId should be of uuid version 4 format', //prisma generates uuidv4
    }),
  }).min(1).messages({
    'object.min': 'To update, enter at least 1 field to change',
  });
  const error = errorCheck(req.body, res, decorSchema);
  if (error) return error;
  next();
};

//----------------LEVEL----------------\\
const validatePostLevel = (req, res, next) => {
  const levelSchema = Joi.object({
    name: Joi.string().max(20).required().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
      'any.required': 'name is required',
    }),
    number: Joi.number().integer().min(0).max(50).required().messages({
      'number.base': 'number should be a number',
      'number.integer': 'number should be a whole number',
      'number.min': 'number should be equal to or greater than {#limit}',
      'number.max': 'number should be equal to or less than {#limit}',
      'number.unsafe': 'number should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
      'any.required': 'number is required',
    }),
    startFloorArea: Joi.number().integer().min(1).max(250).required().messages({
      'number.base': 'startFloorArea should be a number',
      'number.integer': 'startFloorArea should be a whole number',
      'number.min':
        'startFloorArea should be equal to or greater than {#limit}',
      'number.max': 'startFloorArea should be equal to or less than {#limit}',
      'number.unsafe':
        'startFloorArea should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
      'any.required': 'startFloorArea is required',
    }),
    startMoney: Joi.number()
      .integer()
      .min(0)
      .max(2147483647)
      .required()
      .messages({
        'number.base': 'startMoney should be a number',
        'number.integer': 'startMoney should be a whole number',
        'number.min': 'startMoney should be equal to or greater than {#limit}',
        'number.max': 'startMoney should be equal to or less than {#limit}',
        'number.unsafe':
          'startMoney should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
        'any.required': 'startFloorArea is required',
      }),
  });
  const error = errorCheck(req.body, res, levelSchema);
  if (error) return error;
  next();
};

const validatePutLevel = (req, res, next) => {
  const levelSchema = Joi.object({
    name: Joi.string().max(20).optional().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty', //also covers min(1)
      'string.max': 'name should have a maximum length of {#limit}',
    }),
    number: Joi.number().integer().min(0).max(50).optional().messages({
      'number.base': 'number should be a number',
      'number.integer': 'number should be a whole number',
      'number.min': 'number should be equal to or greater than {#limit}',
      'number.max': 'number should be equal to or less than {#limit}',
      'number.unsafe': 'number should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
    }),
    startFloorArea: Joi.number().integer().min(1).max(250).optional().messages({
      'number.base': 'startFloorArea should be a number',
      'number.integer': 'startFloorArea should be a whole number',
      'number.min':
        'startFloorArea should be equal to or greater than {#limit}',
      'number.max': 'startFloorArea should be equal to or less than {#limit}',
      'number.unsafe':
        'startFloorArea should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
    }),
    startMoney: Joi.number()
      .integer()
      .min(0)
      .max(2147483647)
      .optional()
      .messages({
        'number.base': 'startMoney should be a number',
        'number.integer': 'startMoney should be a whole number',
        'number.min': 'startMoney should be equal to or greater than {#limit}',
        'number.max': 'startMoney should be equal to or less than {#limit}',
        //------unsafe runs even if max is implemented, and js safe nums go to 4 quadrillion, where prismas go to 2 trillion------\\
        'number.unsafe':
          'startMoney should be equal to or less than 2,147,483,647', //prisma int (32bit signed) max = 2,147,483,647
      }),
  }).min(1).messages({
    'object.min': 'To update, enter at least 1 field to change',
  });
  const error = errorCheck(req.body, res, levelSchema);
  if (error) return error;
  next();
};

//----------------UTIL METHODS----------------\\
/**
 * @description Checks if req fits the schema
 * @param {object} input - The part of the request object you wish to test, eg. res.body
 * @param {object} res - The response object
 * @param {object} schema - The Joi object
 * @returns Any errors that occur
 */
const errorCheck = (input, res, schema) => {
  const { error } = schema.validate(input);

  if (error) {
    return res.status(STATUS_CODES.VALIDATE_ERROR).json({
      msg: error.details[0].message,
    });
  }
};

export {
  validatePostTank,
  validatePutTank,
  validatePostFish,
  validatePutFish,
  validatePostDecor,
  validatePutDecor,
  validatePostLevel,
  validatePutLevel,
};
