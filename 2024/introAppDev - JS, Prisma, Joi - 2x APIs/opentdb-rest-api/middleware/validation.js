import Joi from 'joi';
import STATUS_CODES from '../../your-choice-rest-api/utils/statusCode.js';
import { categoryIds } from '../utils/categories.js';
import QuizRepo from '../repositories/quiz.js';

const alphaRegex = new RegExp(/^[A-Za-z]+$/);
const msIn1Day = 86400000;
const typeEnum = ['MULTIPLE', 'BOOLEAN'];
const difficultyEnum = ['EASY', 'MEDIUM', 'HARD'];

const validateCreateQuiz = (req, res, next) => {
  //make string dates valid JS date objects
  let startDate = new Date(req.body.startDate);
  req.body.endDate = new Date(req.body.endDate);

  //find the valid end date ranges
  const firstEndDate = new Date(startDate.valueOf() + msIn1Day); //startDate + 1 day
  const finalEndDate = new Date(startDate.valueOf() + msIn1Day * 5); //startDate + 5 days

  //setup startDate to be now, if same date
  const now = new Date(Date.now());
  if (
    startDate.getFullYear() === now.getFullYear() &&
    startDate.getMonth() === now.getMonth() &&
    startDate.getDate() === now.getDate()
  ) {
    req.body.startDate = now;
  } else req.body.startDate = startDate;

  const quizSchema = Joi.object({
    name: Joi.string().min(5).max(30).pattern(alphaRegex).required().messages({
      'string.base': 'name should be a string',
      'string.empty': 'name cannot be empty',
      'string.min': 'name should have a minimum length of {#limit}',
      'string.max': 'name should have a maximum length of {#limit}',
      'string.pattern.base': 'name should only have letters',
      'any.required': 'name is required',
    }),
    type: Joi.any()
      .valid(...Object.values(typeEnum))
      .required()
      .messages({
        'any.only': 'type should be one of {#valids}',
        'any.required': 'type is required',
      }),
    difficulty: Joi.any()
      .valid(...Object.values(difficultyEnum))
      .required()
      .messages({
        'any.only': 'difficulty should be one of {#valids}',
        'any.required': 'difficulty is required',
      }),
    startDate: Joi.date().min(now).required().messages({
      'date.base': 'startDate should be a date in yyyy-mm-dd format',
      'date.min': 'startDate should be today, or in the future',
      'any.required': 'startDate is required',
    }),
    endDate: Joi.date()
      .min(firstEndDate)
      .max(finalEndDate)
      .required()
      .messages({
        'date.base': 'endDate should be a date in yyyy-mm-dd format',
        'date.min': 'endDate should be after startingDate',
        'date.max': 'endDate should not be more than 5 days after startDate',
        'any.required': 'endDate is required',
      }),
    categoryId: Joi.any()
      .valid(...Object.values(categoryIds))
      .required()
      .messages({
        'any.only': 'difficulty should be one of {#valids}',
        'any.required': 'difficulty is required',
      }),
  });

  const error = JoiCheck(req.body, res, quizSchema);
  if (error) return error;
  next();
};

const validatePlayQuiz = async (req, res, next) => {
  const quiz = await QuizRepo.findById(req.params.id);
  if (!quiz) {
    return res.status(STATUS_CODES.NOT_FOUND).json({
      msg: `No Quiz found with the id: ${req.params.id}`,
    });
  } else {
    const now = new Date(Date.now());
    const isWithinDates = now - quiz.startDate >= 0 && quiz.endDate - now >= 0;
    if (!isWithinDates) {
      return res.status(STATUS_CODES.BAD_REQUEST).json({
        msg: 'Cannot play this quiz as is has not started or has finished',
      });
    } else {
      const answerSchema = Joi.object({
        quizAnswers: Joi.array()
          .length(10)
          .items(Joi.string())
          .required()
          .messages({
            'array.base': 'quizAnswers should be an array',
            'array.length': 'quizAnswers should have {#limit} items',
            'any.required': 'quizAnswers array is required',
            'string.base': 'answer should be a string',
            'string.empty': 'answer cannot be empty',
          }),
      });

      const error = JoiCheck(req.body, res, answerSchema);
      if (error) return error;
    }
  }
  next();
};

//----------------UTIL METHODS----------------\\
/**
 * @description Checks if req fits the schema
 * @param {object} input - The part of the request object you wish to test, eg. res.body
 * @param {object} res - The response object
 * @param {object} schema - The Joi object
 * @returns Any errors that occur
 */
const JoiCheck = (input, res, schema) => {
  const { error } = schema.validate(input);

  if (error) {
    return res.status(STATUS_CODES.VALIDATE_ERROR).json({
      msg: error.details[0].message,
    });
  }
};

export { validateCreateQuiz, validatePlayQuiz };
