/**
 * @file connects UserQuizScore to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class UserQuizScoreRepo {
  async create(score, userId, quizId) {
    return await prismaClient.userQuizScore.create({
      data: { score, userId, quizId },
    });
  }

  async findAll() {
    return await prismaClient.userQuizScore.findMany();
  }
}

export default new UserQuizScoreRepo();
