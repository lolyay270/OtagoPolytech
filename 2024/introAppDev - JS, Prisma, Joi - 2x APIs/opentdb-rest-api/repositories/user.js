/**
 * @file connects User to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class UserRepo {
  async findAll() {
    return await prismaClient.user.findMany({
      //enter include/filters/sorting here
    });
  }
}

export default new UserRepo();
