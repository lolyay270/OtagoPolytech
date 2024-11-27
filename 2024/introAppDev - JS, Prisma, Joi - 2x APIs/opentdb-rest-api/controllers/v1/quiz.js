/**
 * @file Manages functions for Quizzes
 * @author Jenna Boyes
 */

import STATUS_CODES from '../../utils/statusCode.js';
import Repo from '../../repositories/quiz.js';
import QuestionRepo from '../../repositories/question.js';
import userQuestionAnswerRepo from '../../repositories/userQuestionAnswer.js';
import userQuizScoreRepo from '../../repositories/userQuizScore.js';
import categoryRepo from '../../repositories/category.js';
import { Prisma } from '../../prisma/prisma.js';
/**
 * @description Create a singular Quiz
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const createQuiz = async (req, res) => {
  try {
    //get questions from OpenTDB based on quiz
    const fetchQuery = `?amount=10&category=${req.body.categoryId}&difficulty=${req.body.difficulty.toLowerCase()}&type=${req.body.type.toLowerCase()}`;
    const fetchRes = await fetch(`https://opentdb.com/api.php${fetchQuery}`);
    const data = await fetchRes.json();

    //check amount of questions fetched
    if (data.results.length !== 10) {
      return res.status(STATUS_CODES.SERVER_ERROR).json({
        msg: `DB fetch for 10 questions only returned ${data.results.length}`,
      });
    }

    //check category exists
    const category = await categoryRepo.findById(req.body.categoryId);
    if (!category) await categoryRepo.create(res, req.body.categoryId);

    const quiz = await Repo.create(req.body);

    const questions = [];
    //create each question into own DB
    for (const question of data.results) {
      const fixedQuestion = {
        question: question.question,
        correctAnswer: question.correct_answer,
        incorrectAnswers: question.incorrect_answers,
        quizId: quiz.id,
      };
      const newQuestion = await QuestionRepo.create(fixedQuestion);
      questions.push(newQuestion);
    }

    //make sure all 10 questions saved to DB correctly
    if (questions.length !== 10) {
      const count = questions.length; //save num to give in error after Qs are deleted

      //delete any generated questions and the quiz
      for (const q of questions) await QuestionRepo.delete(q.id);
      await Repo.delete(quiz.id);

      return res.status(STATUS_CODES.SERVER_ERROR).json({
        msg: `Only ${count} of the 10 questions were saved to the DB correctly`,
      });
    }

    //return all quizzes and status code
    const newQuizzes = await Repo.findAll();
    return res.status(STATUS_CODES.CREATED).json({
      msg: `Quiz successfully created`,
      data: newQuizzes,
    });
  } catch (err) {
    if (err instanceof Prisma.PrismaClientKnownRequestError) {
      if (err.code === 'P2002') {
        return sameFieldExists(res);
      }
    } else {
      return errorResponse(res, err);
    }
  }
};

/**
 * @description Get all Quizzes
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const getAllQuizzes = async (req, res) => {
  try {
    const quizzes = await Repo.findAll();

    if (quizzes.length === 0) {
      return res.status(STATUS_CODES.NOT_FOUND).json({
        msg: `No Quizzes found`,
      });
    }

    return res.json({ data: quizzes });
  } catch (err) {
    return errorResponse(res, err);
  }
};

/**
 * @description Get a singular Quiz, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const getQuiz = async (req, res) => {
  try {
    const quiz = await Repo.findById(req.params.id);

    if (!quiz) {
      return noQuiz(req, res);
    }

    return res.json({
      data: quiz,
    });
  } catch (err) {
    return errorResponse(res, err);
  }
};

/**
 * @description Play a singular quiz, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const playQuiz = async (req, res) => {
  try {
    const quiz = await Repo.findById(req.params.id);
    if (!quiz) {
      return noQuiz(req, res);
    }

    //create answers in DB
    const answers = await userQuestionAnswerRepo.createMany(
      req.user.id,
      quiz,
      req.body.quizAnswers,
    );

    //score, questions, answers, isCorrect, if incorrect give correct answer
    let dataToReturn = {
      score: 0,
      results: {},
    };
    answers.forEach((answer) => {
      let currentQuestion = {};
      currentQuestion.answer = answer.answer;
      currentQuestion.isCorrect = answer.isCorrect;

      dataToReturn.results[answer.questionId] = currentQuestion;

      //count score since were already in a forEach
      if (answer.isCorrect) dataToReturn.score++;
    });

    quiz.questions.forEach((question) => {
      dataToReturn.results[question.id].question = question.question;

      if (!dataToReturn.results[question.id].isCorrect) {
        //if user incorrect
        dataToReturn.results[question.id].correctAnswer =
          question.correctAnswer;
      }
    });

    //create score object in DB
    await userQuizScoreRepo.create(dataToReturn.score, req.user.id, quiz.id);

    return res.status(STATUS_CODES.OK).json({
      msg: `Quiz successfully played`,
      data: dataToReturn,
    });
  } catch (err) {
    return errorResponse(res, err);
  }
};

/**
 * @description Update a singular Quiz, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const updateQuiz = async (req, res) => {
  try {
    let quiz = await Repo.findById(req.params.id);

    if (!quiz) {
      return noQuiz(req, res);
    }

    quiz = await Repo.update(req.params.id, req.body);

    return res.json({
      msg: `Quiz with the id: ${req.params.id} successfully updated`,
      data: quiz,
    });
  } catch (err) {
    if (err instanceof Prisma.PrismaClientKnownRequestError) {
      if (err.code === 'P2002') {
        return sameFieldExists(res);
      }
    } else {
      return errorResponse(res, err);
    }
  }
};

/**
 * @description Delete a singular Quiz, using id
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const deleteQuiz = async (req, res) => {
  try {
    const quiz = await Repo.findById(req.params.id);

    if (!quiz) {
      return noQuiz(req, res);
    }

    await Repo.delete(req.params.id);

    return res.json({
      msg: `Successfully deleted Quiz with the id: ${req.params.id}`,
    });
  } catch (err) {
    return errorResponse(res, err);
  }
};

//--------------- Utility Methods ---------------\\
/**
 * @description This function gives formated error messages
 * @param {object} res - The response object
 * @param {object} err - The catched error
 * @returns {object} - The response object
 */
const errorResponse = (res, err) => {
  return res.status(STATUS_CODES.SERVER_ERROR).json({
    msg: err.message,
  });
};

/**
 * @description This function give "no Quiz found with id: {id}" error message
 * @param {object} req - The request object
 * @param {object} res - The response object
 * @returns {object} The response object
 */
const noQuiz = (req, res) => {
  return res.status(STATUS_CODES.NOT_FOUND).json({
    msg: `No Quiz found with the id: ${req.params.id}`,
  });
};

/**
 * @description This function returns an error message about unique fields
 * @param {object} res - The response object
 * @returns {object} error code with description
 */
const sameFieldExists = (res) => {
  return res.status(STATUS_CODES.ALREADY_EXISTS).json({
    message: 'Quiz with the same ID or name already exists',
  });
};

export { createQuiz, getAllQuizzes, getQuiz, playQuiz, updateQuiz, deleteQuiz };
