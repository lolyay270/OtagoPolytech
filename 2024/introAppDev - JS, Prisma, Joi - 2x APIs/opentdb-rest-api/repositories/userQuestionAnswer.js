/**
 * @file connects UserQuestionAnswer to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class UserQuestionAnswerRepo {
  async createMany(userId, quiz, answers) {
    //data: [{ answer: '...', isCorrrect: ..., ...}, {...}]
    let data = [];

    //set up correct format for schema
    for (let i = 0; i < answers.length; i++) {
      let currentAnswer = {
        answer: answers[i],
        userId,
        quizId: quiz.id,
        questionId: quiz.questions[i].id,
      };
      currentAnswer.isCorrect = answers[i] === quiz.questions[i].correctAnswer;
      data.push(currentAnswer);
    }

    //return all created items instead of { count: 10 }
    return await prismaClient.$transaction(
      data.map((answer) =>
        prismaClient.userQuestionAnswer.create({ data: answer }),
      ),
    );
  }
}

export default new UserQuestionAnswerRepo();
