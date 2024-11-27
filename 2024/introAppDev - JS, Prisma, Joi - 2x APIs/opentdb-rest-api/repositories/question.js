/**
 * @file connects Question to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class QuestionRepo {
  async create(data) {
    //connect quizId correctly with prisma
    //quiz: {connect: {id: [id]} },
    data.quiz = { connect: { id: data.quizId } };
    delete data.quizId;

    return await prismaClient.question.create({
      data,
    });
  }

  async delete(id) {
    return await prismaClient.question.delete({
      where: { id },
    });
  }
}

export default new QuestionRepo();
