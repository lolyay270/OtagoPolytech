/**
 * @file Connects tank functions to Express and Swagger
 * @author Jenna Boyes
 */

import express from 'express';
import {
  createTank,
  getAllTanks,
  getTank,
  updateTank,
  deleteTank,
} from '../../controllers/v1/tank.js';

import {
  validatePostTank,
  validatePutTank,
} from '../../middleware/validation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     Tank:
 *       allOf:
 *       - $ref: '#/components/schemas/BasicTank'
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
 *     BasicTank:
 *       type: object
 *       properties:
 *         name:
 *           type: string
 *           minLength: 1
 *           example: "Basic Tank"
 *         totalFishSize:
 *           type: integer
 *           minimum: 1
 *           example: 8
 *         floorArea:
 *           type: integer
 *           minimum: 1
 *           example: 4
 *         rounded:
 *           type: boolean
 *           example: false
 *         levels:
 *           type: array
 *           items:
 *             type: string
 *             format: uuid
 *             example: "123e4567-e89b-12d3-a456-426614174000"
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
 *                   $ref: '#/components/schemas/BasicTank'
 *
 *     SameFields:
 *       description: Tank with the same ID already exists
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Tank with the same ID already exists"
 *
 *     NoTank:
 *       description: No tank found with provided ID
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "No tank with the ID: {id} found"
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
 * /api/v1/tanks:
 *   post:
 *     summary: Create a new tank
 *     tags:
 *       - Tank
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicTank'
 *
 *     responses:
 *       '201':
 *         description: Tank successfully created
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Tank successfully created"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/Tank'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/', validatePostTank, createTank);

/**
 * @swagger
 * /api/v1/tanks:
 *   get:
 *     summary: Get all tanks
 *     tags:
 *       - Tank
 *
 *     parameters:
 *       - in: query
 *         name: name
 *         schema:
 *           type: string
 *         description: Filter tanks by name
 *       - in: query
 *         name: totalFishSize
 *         schema:
 *           type: integer
 *         description: Filter tanks by totalFishSize
 *       - in: query
 *         name: floorArea
 *         schema:
 *           type: integer
 *         description: Filter tanks by floorArea
 *       - in: query
 *         name: rounded
 *         schema:
 *           type: boolean
 *         description: Filter tanks by rounded
 *       - in: query
 *         name: sortBy
 *         schema:
 *           type: string
 *           enum: [id, name, totalFishSize, floorArea, rounded]
 *         description: Field to sort tank by (default is 'id')
 *       - in: query
 *         name: sortOrder
 *         schema:
 *           type: string
 *           enum: [asc, desc]
 *         description: Order to sort tank by (default is 'asc')
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
 *         description: No tanks found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No tanks found"
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/', getAllTanks);

/**
 * @swagger
 * /api/v1/tanks/{id}:
 *   get:
 *     summary: Get a tank by id
 *     tags:
 *       - Tank
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The tank id
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         $ref: '#/components/responses/NoTank'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/:id', getTank);

/**
 * @swagger
 * /api/v1/tanks/{id}:
 *   put:
 *     summary: Update a tank by id
 *     tags:
 *       - Tank
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The tank id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/BasicTank'
 *
 *     responses:
 *       '200':
 *         description: Tank successfully updated
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Tank with the id: {id} successfully updated"
 *                 data:
 *                   $ref: '#/components/schemas/BasicTank'
 *       '404':
 *         $ref: '#/components/responses/NoTank'
 *
 *       '409':
 *         $ref: '#/components/responses/SameFields'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.put('/:id', validatePutTank, updateTank);

/**
 * @swagger
 * /api/v1/tanks/{id}:
 *   delete:
 *     summary: Delete a tank by id
 *     tags:
 *       - Tank
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The tank id
 *
 *     responses:
 *       '200':
 *         description: Tank successfully deleted
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Tank with the id: {id} successfully deleted"
 *
 *       '404':
 *         $ref: '#/components/responses/NoTank'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.delete('/:id', deleteTank);

export default router;
