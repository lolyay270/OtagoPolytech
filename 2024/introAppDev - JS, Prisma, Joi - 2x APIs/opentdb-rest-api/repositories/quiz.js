/**
 * @file connects Quiz to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class QuizRepo {
  async create(data) {
    return await prismaClient.quiz.create({
      data,
    });
  }

  async findAll() {
    return await prismaClient.quiz.findMany({
      //enter include/filters/sorting here
    });
  }

  async findById(id) {
    return await prismaClient.quiz.findUnique({
      where: { id },
      include: { questions: true },
    });
  }

  async update(id, data) {
    return await prismaClient.quiz.update({
      where: { id },
      data,
    });
  }

  async delete(id) {
    return await prismaClient.quiz.delete({
      where: { id },
    });
  }
}

export default new QuizRepo();
