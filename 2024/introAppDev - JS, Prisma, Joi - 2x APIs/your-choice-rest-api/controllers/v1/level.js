/**
 * @file Manages all functions for Levels
 * @author Jenna Boyes
 */

import {
  createResource,
  getResources,
  getResource,
  updateResource,
  deleteResource,
} from '../base.js';

//store prisma schema values to reuse
const model = 'level'; //the name of the model, all lowercase
const uniqueFields = ['ID', 'number', 'name']; //the unique fields, in plain text (firstName => 'first name')
const include = { startTanks: ['id', 'name'] }; //show which tanks are related
const filters = {
  number: ['number', 'equals'],
  name: ['string', 'contains'],
  startFloorArea: ['number', 'equals'],
  startMoney: ['number', 'equals'],
};
const defaultSort = {
  field: 'id',
  order: 'asc',
};
const query = {
  include,
  filters,
  defaultSort,
};

/**
 * @description Create a new level
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const createLevel = async (req, res) =>
  createResource(req, res, model, uniqueFields);

/**
 * @description Get all level data
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getAllLevels = async (req, res) =>
  getResources(req, res, model, query);

/**
 * @description Get a single level's data by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getLevel = async (req, res) =>
  getResource(req, res, model);

/**
 * @description Update a single level by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const updateLevel = async (req, res) =>
  updateResource(req, res, model, uniqueFields);

/**
 * @description Update a single level by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const deleteLevel = async (req, res) =>
  deleteResource(req, res, model);

// prettier-ignore
export { 
  createLevel,
  getAllLevels,
  getLevel,
  updateLevel,
  deleteLevel,
};
