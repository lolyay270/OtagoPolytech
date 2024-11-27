/**
 * @file Import prisma once, use throughout the project
 * @author Jenna Boyes
 */

import { PrismaClient, Prisma } from '@prisma/client';

const prismaClient = new PrismaClient();

export { prismaClient, Prisma };
