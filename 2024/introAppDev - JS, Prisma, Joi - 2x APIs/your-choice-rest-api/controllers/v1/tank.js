/**
 * @file Manages all functions for Tanks
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
const model = 'tank'; //the name of the model, all lowercase
const uniqueFields = ['ID']; //the unique fields, in plain text (firstName => 'first name')
const include = { levels: ['id', 'number', 'name'] }; //show which levels are related
const filters = {
  name: ['string', 'contains'],
  totalFishSize: ['number', 'equals'],
  floorArea: ['number', 'equals'],
  rounded: ['boolean', 'equals'],
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
 * @description Create a new tank
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const createTank = async (req, res) => {
  //manage assigning tank to many levels
  //should look like: levels = { connect: [{ id: id1 }, { id: id2 }] }
  const setLevels = { connect: [] };
  req.body.levels.forEach((levelId) => {
    setLevels.connect.push({ id: levelId });
  });
  req.body.levels = setLevels;

  createResource(req, res, model, uniqueFields);
};

/**
 * @description Get all tank data
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getAllTanks = async (req, res) =>
  getResources(req, res, model, query);

/**
 * @description Get a single tank's data by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getTank = async (req, res) =>
  getResource(req, res, model);

/**
 * @description Update a single tank by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const updateTank = async (req, res) =>
  updateResource(req, res, model, uniqueFields);

/**
 * @description Update a single tank by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const deleteTank = async (req, res) =>
  deleteResource(req, res, model);

// prettier-ignore
export { 
  createTank,
  getAllTanks,
  getTank,
  updateTank,
  deleteTank,
};
