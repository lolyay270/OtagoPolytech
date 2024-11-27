import { prismaClient } from './prisma.js';

const main = async () => {
  try {
    //---------LEVELS---------\\
    const levelData = [
      {
        number: 1,
        name: 'SunnySide',
        startFloorArea: 63,
        startMoney: 0,
      },
      {
        number: 2,
        name: 'Shallow Beach',
        startFloorArea: 70,
        startMoney: 100,
      },
    ];

    await prismaClient.level.createMany({
      data: levelData,
    });

    const newLevels = await prismaClient.level.findMany();

    //---------TANKS---------\\
    const tankData = [
      {
        name: 'Basic Tank',
        totalFishSize: 8,
        floorArea: 4,
        rounded: false,
        levels: {
          connect: [{ id: newLevels[0].id }],
        },
      },
      {
        name: 'Large Shallow Tank',
        totalFishSize: 20,
        floorArea: 12,
        rounded: false,
        levels: {
          connect: [{ id: newLevels[0].id }, { id: newLevels[1].id }],
        },
      },
    ];

    //creating individually since createMany doesnt allow "connect"
    for (const tank of tankData) {
      await prismaClient.tank.create({
        data: tank,
      });
    }
  } catch (err) {
    console.error(err);
  } finally {
    await prismaClient.$disconnect();
    process.exit(0);
  }
};

main();
