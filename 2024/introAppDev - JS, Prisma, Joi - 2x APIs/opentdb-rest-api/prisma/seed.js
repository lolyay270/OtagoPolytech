/**
 * @file Seed 4 Users into DB
 * @author Jenna Boyes
 */

import { prismaClient } from "./prisma.js";
import bcryptjs from "bcryptjs";

const main = async () => {
  try {
    const userData = [
      {
        firstName: "John",
        lastName: "Doe",
        emailAddress: "john.doe@example.com",
        password: "password123",
        role: "ADMIN",
      },
      {
        firstName: "Jane",
        lastName: "Doe",
        emailAddress: "jane.doe@example.com",
        password: "password123",
        role: "ADMIN",
      },
      {
        firstName: "Randy",
        lastName: "Doe",
        emailAddress: "randy.doe@example.com",
        password: "password123",
        role: "BASIC",
      },
      {
        firstName: "Sam",
        lastName: "Doe",
        emailAddress: "sam.doe@example.com",
        password: "password123",
        role: "BASIC",
      },
      {
        firstName: "Bob",
        lastName: "Doe",
        emailAddress: "bob.doe@example.com",
        password: "password123",
        role: "BASIC",
      },
    ];

    const newUserData = await Promise.all(
      userData.map(async (user) => {
        const salt = await bcryptjs.genSalt();
        const hashedPassword = await bcryptjs.hash(user.password, salt);
        return { ...user, password: hashedPassword };
      })
    );

    await prismaClient.user.createMany({
      data: newUserData,
    });
  } catch (err) {
    console.error(err);
  } finally {
    await prismaClient.$disconnect();
    process.exit(0);
  }
};

main();