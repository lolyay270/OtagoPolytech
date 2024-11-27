/**
 * @file Connects institutions to prisma methods
 * @author Jenna Boyes
 */

import { prismaClient } from '../prisma/prisma.js';

class BaseRepo {
  create = async (data, model) => {
    return await prismaClient[model].create({
      data,
    });
  };

  /**
   * @description Get all data for the given model through prisma
   * @param {string} model - The data's model
   * @param {object} reqQuery - The req.query object
   * @param {object} baseQuery - The parent query object
   * @param {object} [baseQuery.include] - The relation to include, eg. { home: 'all', school: ['name', 'address'], }
   * @param {object} [baseQuery.filters] - The fields to filter, eg { name: ['string', 'contains'], age: ['number', 'equals'], }
   * @param {object} [baseQuery.defaultSort] - The sorting of fields, eg. { field: 'firstName', order 'asc', }
   * @returns All data for the given model, if any exist
   */
  findAll = async (model, reqQuery, baseQuery) => {
    const finalQuery = {};

    //check base query even exists
    if (baseQuery) {

      //--------fix include into prisma format--------\\
      // all fields from relation      = include: { school: true }
      // selected fields from relation = select: { school: { select: { name: true }, { address: true }}}
      if (baseQuery.include) {
        //setup a list of fieldNames for model, since req.body is empty
        const resource = await prismaClient[model].findFirst();
        //dont run if there is no data
        if (resource) {
          const allFieldNames = Object.keys(resource);

          for (const [relation, fields] of Object.entries(baseQuery.include)) {
            //-------selected fields
            if (typeof fields === 'object') {
              //setup object structure
              if (!finalQuery.select) finalQuery.select = {};
              if (!finalQuery.select[relation]) finalQuery.select[relation] = {};
              if (!finalQuery.select[relation].select)
                finalQuery.select[relation].select = {};

              //select all of the model's fields (when add relation select, the model's fields are ignored)
              allFieldNames.forEach((field) => {
                if (!finalQuery.select[field]) finalQuery.select[field] = true; //dont set again if many relations
              });

              //add relation feilds
              fields.forEach((field) => {
                finalQuery.select[relation].select[field] = true;
              });
            }

            //-------all fields
            else if (fields === 'all') {
              if (!finalQuery.include) finalQuery.include = {};
              finalQuery.include[relation] = true;
            }
          }
        }
      }

      //--------fix filter into prisma format { name: { contains: 'bob' }}--------\\
      if (baseQuery.filters && Object.keys(reqQuery).length > 0) {
        finalQuery.where = {};

        //take fieldNames from baseQuery so dont get sorting or other inputs
        for (const [fieldName, value] of Object.entries(baseQuery.filters)) {
          //dont add unless it is being filtered
          if (Object.keys(reqQuery).includes(fieldName)) {
            const dataType = value[0];
            const filterCondition = value[1];
            const input = reqQuery[fieldName];

            if (dataType === 'string') {
              finalQuery.where[fieldName] = { [filterCondition]: input };
            } else if (dataType === 'number') {
              finalQuery.where[fieldName] = { [filterCondition]: Number(input) };
            } else if (dataType === 'boolean') {
              finalQuery.where[fieldName] = { [filterCondition]: Boolean(input) };
            }
          }
        }
      }

      //--------fix sorting into prisma format { orderBy: name: asc }--------\\
      const sortBy = reqQuery.sortBy || baseQuery.defaultSort.field;
      const sortOrder = reqQuery.sortOrder || baseQuery.defaultSort.order;
      finalQuery.orderBy = { [sortBy]: sortOrder };

      //-------fix pagination into prisma format { skip: 8, take: 4 }--------\\
      const take = Number(reqQuery.pageSize) || 25; //default is 25
      const skip = (Number(reqQuery.pageNum) - 1) * take || 0; //default is first page, pageNum: 3 = skip: take * 2
      finalQuery.take = take;
      finalQuery.skip = skip;
    }
    
    return await prismaClient[model].findMany(finalQuery);
  };

  findById = async (id, model) => {
    return await prismaClient[model].findUnique({
      where: { id },
    });
  };

  update = async (id, data, model) => {
    return await prismaClient[model].update({
      where: { id },
      data,
    });
  };

  delete = async (id, model) => {
    return await prismaClient[model].delete({
      where: { id },
    });
  };
}

export default new BaseRepo();
