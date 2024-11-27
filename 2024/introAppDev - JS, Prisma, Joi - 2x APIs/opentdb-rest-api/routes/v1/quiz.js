/**
 * @file Connects Quiz controller to express and swagger
 * @author Jenna Boyes
 */

import express from 'express';
import {
  createQuiz,
  getAllQuizzes,
  getQuiz,
  playQuiz,
  updateQuiz,
  deleteQuiz,
} from '../../controllers/v1/quiz.js';
import {
  validateCreateQuiz,
  validatePlayQuiz,
} from '../../middleware/validation.js';
import adminAuthorisation from '../../middleware/adminAuthorisation.js';
import basicAuthorisation from '../../middleware/basicAuthorisation.js';

const router = express.Router();

/**
 * @swagger
 * components:
 *   schemas:
 *     Quiz:
 *       allOf:
 *       - $ref: '#/components/schemas/QuizBasic'
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
 *     QuizBasic:
 *       type: object
 *       properties:
 *         name:
 *           type: string
 *           example: "QuizName"
 *         type:
 *           type: string
 *           enum: [MULTIPLE, BOOLEAN]
 *           example: MULTIPLE
 *         difficulty:
 *           type: string
 *           enum: [EASY, MEDIUM, HARD]
 *           example: EASY
 *         startDate:
 *           type: string
 *           format: date-time
 *           example: "2024-12-30"
 *         endDate:
 *           type: string
 *           format: date-time
 *           example: "2024-12-31"
 *         categoryId:
 *           type: integer
 *           example: 9
 *
 *     QuizAnswer:
 *       type: object
 *       properties:
 *         quizAnswers:
 *           type: array
 *           items:
 *             type: string
 *             example: "answer for question 1"
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
 *                   $ref: '#/components/schemas/Quiz'
 *
 *     SameField:
 *       description: Quiz with the same ID or name already exists
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "Quiz with the same ID or name already exists"
 *
 *     NoQuiz:
 *       description: No quiz found with the provided id
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *               message:
 *                 type: string
 *                 example: "No quiz found with the id: {id}"
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
 *
 *   securitySchemes:
 *     BearerAuth:
 *       type: http
 *       scheme: bearer
 *       bearerFormat: JWT
 *   security:
 *     - BearerAuth: []
 */

//------------------CREATE------------------\\
/**
 * @swagger
 * /api/v1/quizzes:
 *   post:
 *     summary: Create a new quiz
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/QuizBasic'
 *
 *     responses:
 *       '201':
 *         description: Quiz successfully created
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Quiz successfully created"
 *                 data:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/Quiz'
 *       '409':
 *         $ref: '#/components/responses/SameField'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/', validateCreateQuiz, adminAuthorisation, createQuiz);

//------------------GET ALL------------------\\
/**
 * @swagger
 * /api/v1/quizzes:
 *   get:
 *     summary: Get all quizzes
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         description: No quizzes found
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "No quizzes found"
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/', getAllQuizzes);

//------------------GET ONE------------------\\
/**
 * @swagger
 * /api/v1/quizzes/{id}:
 *   get:
 *     summary: Get a quiz by id
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The quiz id
 *
 *     responses:
 *       '200':
 *         $ref: '#/components/responses/GetSuccess'
 *
 *       '404':
 *         $ref: '#/components/responses/NoQuiz'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.get('/:id', getQuiz);

//------------------PLAY------------------\\
/**
 * @swagger
 * /api/v1/quizzes/{id}:
 *   post:
 *     summary: Play and answer quiz questions by id
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The quiz id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/QuizAnswer'
 *
 *     responses:
 *       '200':
 *         description: Quiz successfully played
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "successfully played Quiz with the id: {id}"
 *                 data:
 *                   $ref: '#/components/schemas/QuizAnswer'
 *
 *       '404':
 *         $ref: '#/components/responses/NoQuiz'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.post('/:id', validatePlayQuiz, basicAuthorisation, playQuiz);

//------------------UPDATE------------------\\
/**
 * @swagger
 * /api/v1/quizzes/{id}:
 *   put:
 *     summary: Update a quiz by id
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The quiz id
 *
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/QuizBasic'
 *
 *     responses:
 *       '200':
 *         description: Quiz successfully updated
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "Successfully updated Quiz with the id: {id}"
 *                 data:
 *                   $ref: '#/components/schemas/Quiz'
 *       '404':
 *         $ref: '#/components/responses/NoQuiz'
 *
 *       '409':
 *         $ref: '#/components/responses/SameField'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.put('/:id', adminAuthorisation, updateQuiz);

//------------------DELETE------------------\\
/**
 * @swagger
 * /api/v1/quizzes/{id}:
 *   delete:
 *     summary: Delete a quiz by id
 *     tags:
 *       - Quiz
 *
 *     security:
 *       - BearerAuth: []
 *
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The quiz id
 *
 *     responses:
 *       '200':
 *         description: Quiz successfully deleted
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 message:
 *                   type: string
 *                   example: "successfully deleted Quiz with the id: {id}"
 *
 *       '404':
 *         $ref: '#/components/responses/NoQuiz'
 *
 *       '500':
 *         $ref: '#/components/responses/ServerError'
 */
router.delete('/:id', adminAuthorisation, deleteQuiz);

export default router;
