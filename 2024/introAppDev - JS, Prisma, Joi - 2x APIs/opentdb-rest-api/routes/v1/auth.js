/**
 * @file Connects Authentication controller to express and swagger
 * @author Jenna Boyes
 */
import { Router } from 'express';
import { register, login } from '../../controllers/v1/auth.js';

const router = Router();

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
 *           loginAttempts:
 *             type: integer
 *             example: 3
 *           lastLoginAttempt:
 *             type: string
 *             format: date-time
 *             example: "2024-07-14T12:34:56Z"
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
 *     Password:
 *       type: object
 *       properties:
 *         password:
 *           type: string
 *           format: password
 *           example: "YourPasswordHere123"
 *
 *   responses:
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
 * /api/v1/auth/register:
 *   post:
 *     summary: Register a new user
 *     tags:
 *       - Auth
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             allOf:
 *             - $ref: '#/components/schemas/UserBasic'
 *             - $ref: '#/components/schemas/Password'
 *     responses:
 *       '201':
 *         description: User successfully registered
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   example: "User successfully registered"
 *                 data:
 *                   $ref: '#/components/schemas/UserBasic'
 *       '409':
 *         description: User already exists
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   example: "User already exists"
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.route('/register').post(register);

/**
 * @swagger
 * /api/v1/auth/login:
 *   post:
 *     summary: Log in an existing user
 *     tags:
 *       - Auth
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               emailAddress:
 *                 type: string
 *                 format: email
 *                 example: "john.doe@example.com"
 *               password:
 *                 type: string
 *                 example: "password123"
 *     responses:
 *       '200':
 *         description: User successfully logged in
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   example: "User successfully logged in"
 *                 user:
 *                   type: object
 *                   properties:
 *                     id:
 *                       type: string
 *                       format: uuid
 *                       example: "123e4567-e89b-12d3-a456-426614174000"
 *                     firstName:
 *                       type: string
 *                       example: "John"
 *                     lastName:
 *                       type: string
 *                       example: "Doe"
 *                     emailAddress:
 *                       type: string
 *                       format: email
 *                       example: "john.doe@example.com"
 *                 token:
 *                   type: string
 *                   example: ""
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.route('/login').post(login);

export default router;
