/**
 * @file Connects level functions to Express and Swagger
 * @author Jenna Boyes
 */

import express from 'express';
import {
  createLevel,
  getAllLevels,
  getLevel,
  updateLevel,
  deleteLevel,
} from '../../controllers/v1/level.js';
import {
  validatePostLevel,
  validatePutLevel,
} from '../../middleware/validation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     Level:
 *       allOf:
 *       - $ref: '#/components/schemas/BasicLevel'
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
 *     BasicLevel:
 *       type: object
 *       properties:
 *         name:
 *           type: string
 *           minLength: 1
 *           example: "SunnySide"
 *         number:
 *           type: integer
 *           minimum: 1
 *           example: 1
 *         startFloorArea:
 *           type: integer
 *           minimum: 1
 *           example: 63
 *         startMoney:
 *           type: integer
 *           minimum: 0
 *           example: 0
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
 *                   $ref: '#/components/schemas/BasicLevel'
 *
 *     SameFields:
 *       description: Level with the same ID, number or name already exists
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Level with the same ID, number or name already exists"
 *
 *     NoLevel:
 *       description: No level found with provided ID
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "No level with the ID: {id} found"
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
 * /api/v1/levels:
 *   post:
 *     summary: Create a new level
 *     tags:
 *       - Level
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicLevel'
 *
 *     responses:
 *       '201':
 *         description: Level successfully created
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Level successfully created"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/Level'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/', validatePostLevel, createLevel);

/**
 * @swagger
 * /api/v1/levels:
 *   get:
 *     summary: Get all levels
 *     tags:
 *       - Level
 *
 *     parameters:
 *       - in: query
 *         name: number
 *         schema:
 *           type: integer
 *         description: Filter levels by number
 *       - in: query
 *         name: name
 *         schema:
 *           type: string
 *         description: Filter levels by name
 *       - in: query
 *         name: startFloorArea
 *         schema:
 *           type: integer
 *         description: Filter levels by startFloorArea
 *       - in: query
 *         name: startMoney
 *         schema:
 *           type: integer
 *         description: Filter levels by startMoney
 *       - in: query
 *         name: sortBy
 *         schema:
 *           type: string
 *           enum: [id, number, name, startFloorArea, startMoney]
 *         description: Field to sort level by (default is 'id')
 *       - in: query
 *         name: sortOrder
 *         schema:
 *           type: string
 *           enum: [asc, desc]
 *         description: Order to sort level by (default is 'asc')
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
 *         description: No levels found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No levels found"
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/', getAllLevels);

/**
 * @swagger
 * /api/v1/levels/{id}:
 *   get:
 *     summary: Get a level by id
 *     tags:
 *       - Level
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The level id
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         $ref: '#/components/responses/NoLevel'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/:id', getLevel);

/**
 * @swagger
 * /api/v1/levels/{id}:
 *   put:
 *     summary: Update a level by id
 *     tags:
 *       - Level
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The level id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicLevel'
 *
 *     responses:
 *       '200':
 *         description: Level successfully updated
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Level with the id: {id} successfully updated"
 *                 data:
 *                   $ref: '#/components/schemas/BasicLevel'
 *       '404':
 *         $ref: '#/components/responses/NoLevel'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.put('/:id', validatePutLevel, updateLevel);

/**
 * @swagger
 * /api/v1/levels/{id}:
 *   delete:
 *     summary: Delete a level by id
 *     tags:
 *       - Level
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The level id
 *
 *     responses:
 *       '200':
 *         description: Level successfully deleted
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Level with the id: {id} successfully deleted"
 *
 *       '404':
 *         $ref: '#/components/responses/NoLevel'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.delete('/:id', deleteLevel);

export default router;
