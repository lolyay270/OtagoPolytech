/**
 * @file Connects User controller to express and swagger
 * @author Jenna Boyes
 */

import express from 'express';
import { getAllUsers } from '../../controllers/v1/user.js';
import adminAuthorisation from '../../middleware/adminAuthorisation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     UserNoPassword:
 *       allOf:
 *       - $ref: '#/components/schemas/UserBasic'
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
 *     UserBasic:
 *       type: object
 *       properties:
 *         firstName:
 *           type: string
 *           example: "John"
 *         lastName:
 *           type: string
 *           example: "Doe"
 *         emailAddress:
 *           type: string
 *           format: email
 *           example: "email@website.com"
 *         role:
 *           type: string
 *           enum: [BASIC, ADMIN]
 *           example: BASIC
 *
 *   securitySchemes:
 *     BearerAuth:
 *       type: http
 *       scheme: bearer
 *       bearerFormat: JWT
 *   security:
 *     - BearerAuth: []
 */

//------------------GET ALL------------------\\
/**
 * @swagger
 * /api/v1/users:
 *   get:
 *     summary: Get all users
 *     tags:
 *       - User
 *
 *     security:
 *       - BearerAuth: []
 *
 *     responses:
 *       '200':
 *         description: Success
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Success"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/UserNoPassword'
 *
 *       '404':
 *         description: No users found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No users found"
 *       '500':
 *         description: Internal server error
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "An unexpected error occurred"
 */
router.get('/', adminAuthorisation, getAllUsers);

export default router;
