/*
  Warnings:

  - Made the column `plantAmount` on table `Decor` required. This step will fail if there are existing NULL values in that column.
  - Made the column `rockAmount` on table `Decor` required. This step will fail if there are existing NULL values in that column.
  - Made the column `caveAmount` on table `Decor` required. This step will fail if there are existing NULL values in that column.

*/
-- AlterTable
ALTER TABLE "Decor" ALTER COLUMN "plantAmount" SET NOT NULL,
ALTER COLUMN "rockAmount" SET NOT NULL,
ALTER COLUMN "caveAmount" SET NOT NULL;
