/**
 * @file Connects UserQuizScore controller to express and swagger
 * @author Jenna Boyes
 */

import express from 'express';
import { getAllScores } from '../../controllers/v1/userQuizScore.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     UserQuizScore:
 *       type: object
 *       properties:
 *         id:
 *           type: string
 *           format: uuid
 *           example: "123e4567-e89b-12d3-a456-426614174000"
 *         score:
 *           type: integer
 *           example: 5
 *         userId:
 *           type: string
 *           format: uuid
 *           example: "123e4567-e89b-12d3-a456-426614174000"
 *         quizId:
 *           type: string
 *           format: uuid
 *           example: "123e4567-e89b-12d3-a456-426614174000"
 *         createdAt:
 *           type: string
 *           format: date-time
 *           example: "2024-07-14T12:34:56Z"
 *         updatedAt:
 *           type: string
 *           format: date-time
 *           example: "2024-07-14T12:34:56Z"
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
 * /api/v1/scores:
 *   get:
 *     summary: Get all scores
 *     tags:
 *       - UserQuizScore
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
 *                     $ref: '#/components/schemas/UserQuizScore'
 *
 *       '404':
 *         description: No scores found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No scores found"
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
router.get('/', getAllScores);

export default router;
