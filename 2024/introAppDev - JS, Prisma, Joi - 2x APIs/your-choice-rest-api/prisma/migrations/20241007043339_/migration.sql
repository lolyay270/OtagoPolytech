-- CreateEnum
CREATE TYPE "Temperature" AS ENUM ('COLD', 'TROPICAL');

-- CreateTable
CREATE TABLE "Level" (
    "id" TEXT NOT NULL,
    "number" INTEGER NOT NULL,
    "name" TEXT NOT NULL,
    "startFloorArea" INTEGER NOT NULL,
    "startMoney" INTEGER NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Level_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Tank" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "totalFishSize" INTEGER NOT NULL,
    "floorArea" INTEGER NOT NULL,
    "rounded" BOOLEAN NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Tank_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Fish" (
    "id" TEXT NOT NULL,
    "scientificName" TEXT NOT NULL,
    "commonName" TEXT,
    "maxSize" INTEGER NOT NULL,
    "temperature" "Temperature" NOT NULL,
    "tankId" TEXT NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Fish_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Decor" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "plantAmount" INTEGER,
    "rockAmount" INTEGER,
    "caveAmount" INTEGER,
    "floorArea" INTEGER NOT NULL,
    "tankId" TEXT NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Decor_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "_LevelToTank" (
    "A" TEXT NOT NULL,
    "B" TEXT NOT NULL
);

-- CreateIndex
CREATE UNIQUE INDEX "Level_number_key" ON "Level"("number");

-- CreateIndex
CREATE UNIQUE INDEX "Level_name_key" ON "Level"("name");

-- CreateIndex
CREATE UNIQUE INDEX "_LevelToTank_AB_unique" ON "_LevelToTank"("A", "B");

-- CreateIndex
CREATE INDEX "_LevelToTank_B_index" ON "_LevelToTank"("B");

-- AddForeignKey
ALTER TABLE "Fish" ADD CONSTRAINT "Fish_tankId_fkey" FOREIGN KEY ("tankId") REFERENCES "Tank"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Decor" ADD CONSTRAINT "Decor_tankId_fkey" FOREIGN KEY ("tankId") REFERENCES "Tank"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "_LevelToTank" ADD CONSTRAINT "_LevelToTank_A_fkey" FOREIGN KEY ("A") REFERENCES "Level"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "_LevelToTank" ADD CONSTRAINT "_LevelToTank_B_fkey" FOREIGN KEY ("B") REFERENCES "Tank"("id") ON DELETE CASCADE ON UPDATE CASCADE;
