/**
 * @file connects Category to the prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';
import { convertIdToName } from '../controllers/v1/category.js';

class CategoryRepo {
  async create(res, id) {
    const name = await convertIdToName(res, id);
    const data = {
      id,
      name,
    };
    return await prismaClient.category.create({
      data,
    });
  }

  async findById(id) {
    return await prismaClient.category.findUnique({
      where: { id },
    });
  }
}

export default new CategoryRepo();
