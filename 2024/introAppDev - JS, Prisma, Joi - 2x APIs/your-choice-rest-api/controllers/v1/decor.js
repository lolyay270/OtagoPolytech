/**
 * @file Manages all functions for Decors
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
const model = 'decor'; //the name of the model, all lowercase
const uniqueFields = ['ID']; //the unique fields, in plain text (firstName => 'first name')
const filters = {
  name: ['string', 'contains'],
  plantAmount: ['number', 'equals'],
  rockAmount: ['number', 'equals'],
  caveAmount: ['number', 'equals'],
  floorArea: ['number', 'equals'],
  tankId: ['string', 'equals'],
};
const defaultSort = {
  field: 'id',
  order: 'asc',
};
const query = {
  filters,
  defaultSort,
};

/**
 * @description Create a new decor
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const createDecor = async (req, res) =>
  createResource(req, res, model, uniqueFields);

/**
 * @description Get all decor data
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getAllDecors = async (req, res) =>
  getResources(req, res, model, query);

/**
 * @description Get a single decor's data by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getDecor = async (req, res) =>
  getResource(req, res, model);

/**
 * @description Update a single decor by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
const updateDecor = async (req, res) =>
  updateResource(req, res, model, uniqueFields);

/**
 * @description Update a single decor by name
 * @param {object} req - The request object
 * @param {*} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const deleteDecor = async (req, res) =>
  deleteResource(req, res, model);

// prettier-ignore
export { 
  createDecor,
  getAllDecors,
  getDecor,
  updateDecor,
  deleteDecor,
};
