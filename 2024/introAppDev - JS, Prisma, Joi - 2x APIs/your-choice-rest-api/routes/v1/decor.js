/**
 * @file Connects decor functions to Express and Swagger
 * @author Jenna Boyes
 */

import express from 'express';
import {
  createDecor,
  getAllDecors,
  getDecor,
  updateDecor,
  deleteDecor,
} from '../../controllers/v1/decor.js';
import {
  validatePostDecor,
  validatePutDecor,
} from '../../middleware/validation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     Decor:
 *       allOf:
 *       - $ref: '#/components/schemas/BasicDecor'
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
 *     BasicDecor:
 *       type: object
 *       properties:
 *         name:
 *           type: string
 *           example: "Sea Lettuce"
 *         plantAmount:
 *           type: integer
 *           example: 1
 *         rockAmount:
 *           type: integer
 *           example: 0
 *         caveAmount:
 *           type: integer
 *           example: 0
 *         floorArea:
 *           type: integer
 *           example: 1
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
 *                   $ref: '#/components/schemas/Decor'
 *
 *     SameFields:
 *       description: Decor with the same ID already exists
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Decor with the same ID already exists"
 *
 *     NoDecor:
 *       description: No decor found with provided ID
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "No decor with the ID: {id} found"
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
 * /api/v1/decors:
 *   post:
 *     summary: Create a new decor
 *     tags:
 *       - Decor
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicDecor'
 *
 *     responses:
 *       '201':
 *         description: Decor successfully created
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Decor successfully created"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/Decor'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/', validatePostDecor, createDecor);

/**
 * @swagger
 * /api/v1/decors:
 *   get:
 *     summary: Get all decors
 *     tags:
 *       - Decor
 *
 *     parameters:
 *       - in: query
 *         name: name
 *         schema:
 *           type: string
 *         description: Filter decor by name
 *       - in: query
 *         name: plantAmount
 *         schema:
 *           type: integer
 *         description: Filter decor by plantAmount
 *       - in: query
 *         name: rockAmount
 *         schema:
 *           type: integer
 *         description: Filter decor by rockAmount
 *       - in: query
 *         name: caveAmount
 *         schema:
 *           type: integer
 *         description: Filter decor by caveAmount
 *       - in: query
 *         name: floorArea
 *         schema:
 *           type: integer
 *         description: Filter decor by floorArea
 *       - in: query
 *         name: tankId
 *         schema:
 *           type: string
 *         description: Filter decor by tankId
 *       - in: query
 *         name: sortBy
 *         schema:
 *           type: string
 *           enum: [id, name, plantAmount, rockAmount, caveAmount, floorArea, tankId]
 *         description: Field to sort decor by (default is 'id')
 *       - in: query
 *         name: sortOrder
 *         schema:
 *           type: string
 *           enum: [asc, desc]
 *         description: Order to sort decor by (default is 'asc')
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
 *         description: No decors found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No decors found"
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/', getAllDecors);

/**
 * @swagger
 * /api/v1/decors/{id}:
 *   get:
 *     summary: Get a decor by id
 *     tags:
 *       - Decor
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The decor id
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         $ref: '#/components/responses/NoDecor'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/:id', getDecor);

/**
 * @swagger
 * /api/v1/decors/{id}:
 *   put:
 *     summary: Update a decor by id
 *     tags:
 *       - Decor
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The decor id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicDecor'
 *
 *     responses:
 *       '200':
 *         description: Decor successfully updated
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Decor with the id: {id} successfully updated"
 *                 data:
 *                   $ref: '#/components/schemas/Decor'
 *       '404':
 *         $ref: '#/components/responses/NoDecor'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.put('/:id', validatePutDecor, updateDecor);

/**
 * @swagger
 * /api/v1/decors/{id}:
 *   delete:
 *     summary: Delete a decor by id
 *     tags:
 *       - Decor
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The decor id
 *
 *     responses:
 *       '200':
 *         description: Decor successfully deleted
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Decor with the id: {id} successfully deleted"
 *
 *       '404':
 *         $ref: '#/components/responses/NoDecor'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.delete('/:id', deleteDecor);

export default router;
