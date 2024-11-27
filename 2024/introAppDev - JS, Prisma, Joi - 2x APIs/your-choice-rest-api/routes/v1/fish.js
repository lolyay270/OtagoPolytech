/**
 * @file Connects fish functions to Express and Swagger
 * @author Jenna Boyes
 */

import express from 'express';
import {
  createFish,
  getAllFishes,
  getFish,
  updateFish,
  deleteFish,
} from '../../controllers/v1/fish.js';
import {
  validatePostFish,
  validatePutFish,
} from '../../middleware/validation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     Fish:
 *       allOf:
 *       - $ref: '#/components/schemas/BasicFish'
 *       - type: object
 *         properties:
 *           id:
 *             type: string
 *             format: uuid
 *             example: "123e4567-e89b-12d3-a456-426614174000"
 *           createdAt:
 *             type: string
 *             format: date-time
 *             example: "2024-07-14T12:34:56Z"
 *           updatedAt:
 *             type: string
 *             format: date-time
 *             example: "2024-07-14T12:34:56Z"
 *
 *     BasicFish:
 *       type: object
 *       properties:
 *         scientificName:
 *           type: string
 *           minLength: 1
 *           example: "Chrysiptera hemicyanea"
 *         commonName:
 *           type: string
 *           example: "Azure Demoiselle"
 *         maxSize:
 *           type: integer
 *           minimum: 1
 *           example: 2
 *         temperature:
 *           type: string
 *           enum: [TROPICAL, COLD]
 *           example: TROPICAL
 *         tankId:
 *           type: string
 *           format: uuid
 *           example: "123e4567-e89b-12d3-a456-426614174000"
 *
 *   responses:
 *     GetSuccess:
 *       description: Success
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Success"
 *               data:
 *                 type: array
 *                 items:
 *                   $ref: '#/components/schemas/BasicFish'
 *
 *     SameFields:
 *       description: Fish with the same ID already exists
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Fish with the same ID already exists"
 *
 *     NoFish:
 *       description: No fish found with provided ID
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "No fish with the ID: {id} found"
 *
 *     ServerError:
 *       description: Internal server error
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "An unexpected error occurred"
 */

/**
 * @swagger
 * /api/v1/fishes:
 *   post:
 *     summary: Create a new fish
 *     tags:
 *       - Fish
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicFish'
 *
 *     responses:
 *       '201':
 *         description: Fish successfully created
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Fish successfully created"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/Fish'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/', validatePostFish, createFish);

/**
 * @swagger
 * /api/v1/fishes:
 *   get:
 *     summary: Get all fishes
 *     tags:
 *       - Fish
 *
 *     parameters:
 *       - in: query
 *         name: scientificName
 *         schema:
 *           type: string
 *         description: Filter fish by scientificName
 *       - in: query
 *         name: commonName
 *         schema:
 *           type: string
 *         description: Filter fish by commonName
 *       - in: query
 *         name: maxSize
 *         schema:
 *           type: integer
 *         description: Filter fish by maxSize
 *       - in: query
 *         name: temperature
 *         schema:
 *           type: string
 *           enum: [TROPICAL, COLD]
 *         description: Filter fish by temperature
 *       - in: query
 *         name: tankId
 *         schema:
 *           type: string
 *         description: Filter fish by tankId
 *       - in: query
 *         name: sortBy
 *         schema:
 *           type: string
 *           enum: [id, scientificName, commonName, maxSize, temperature, tankId]
 *         description: Field to sort fish by (default is 'id')
 *       - in: query
 *         name: sortOrder
 *         schema:
 *           type: string
 *           enum: [asc, desc]
 *         description: Order to sort fish by (default is 'asc')
 *       - in: query
 *         name: pageSize
 *         schema:
 *           type: integer
 *         description: How many entries to show per page (default is 25)
 *       - in: query
 *         name: pageNum
 *         schema:
 *           type: integer
 *         description: Which page of entries to show (default is 1)
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         description: No fishes found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No fishes found"
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/', getAllFishes);

/**
 * @swagger
 * /api/v1/fishes/{id}:
 *   get:
 *     summary: Get a fish by id
 *     tags:
 *       - Fish
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The fish id
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         $ref: '#/components/responses/NoFish'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/:id', getFish);

/**
 * @swagger
 * /api/v1/fishes/{id}:
 *   put:
 *     summary: Update a fish by id
 *     tags:
 *       - Fish
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The fish id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicFish'
 *
 *     responses:
 *       '200':
 *         description: Fish successfully updated
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Fish with the id: {id} successfully updated"
 *                 data:
 *                   $ref: '#/components/schemas/BasicFish'
 *       '404':
 *         $ref: '#/components/responses/NoFish'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.put('/:id', validatePutFish, updateFish);

/**
 * @swagger
 * /api/v1/fishes/{id}:
 *   delete:
 *     summary: Delete a fish by id
 *     tags:
 *       - Fish
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The fish id
 *
 *     responses:
 *       '200':
 *         description: Fish successfully deleted
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Fish with the id: {id} successfully deleted"
 *
 *       '404':
 *         $ref: '#/components/responses/NoFish'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.delete('/:id', deleteFish);

export default router;
