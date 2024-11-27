/**
 * @file Manages all functions for Fishes
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
const model = 'fish'; //the name of the model, all lowercase
const uniqueFields = ['ID']; //the unique fields, in plain text (firstName => 'first name')
const filters = { //how to filter this model in get methods
  scientificName: ['string', 'contains'],
  commonName: ['string', 'contains'],
  maxSize: ['number', 'equals'],
  temperature: ['string', 'equals'],
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
 * @description Create a new fish
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const createFish = async (req, res) =>
  createResource(req, res, model, uniqueFields);

/**
 * @description Get all fish data
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getAllFishes = async (req, res) =>
  getResources(req, res, model, query);

/**
 * @description Get a single fish's data by name
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const getFish = async (req, res) =>
  getResource(req, res, model);

/**
 * @description Update a single fish by name
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const updateFish = async (req, res) =>
  updateResource(req, res, model, uniqueFields);

/**
 * @description Update a single fish by name
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
// prettier-ignore
const deleteFish = async (req, res) =>
  deleteResource(req, res, model);

// prettier-ignore
export { 
  createFish,
  getAllFishes,
  getFish,
  updateFish,
  deleteFish,
};
