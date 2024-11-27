/**
 * @file Test functions for Decors
 * @author Jenna Boyes
 */

import chai from 'chai';
import chaiHttp from 'chai-http';
import { describe, it, before } from 'mocha';
import app from '../app.js';
import STATUS_CODES from '../utils/statusCode.js';
import { prismaClient } from '../prisma/prisma.js';

chai.use(chaiHttp);

const tankIds = await prismaClient.tank.findMany({ select: { id: true } }); //items 0 and 1 are seeded
let decorId;

describe('Decors', () => {
  //create
  it('should create Decor', (done) => {
    chai
      .request(app)
      .post('/api/v1/decors')
      .send({
        name: 'Mossy Rocks',
        plantAmount: 1,
        rockAmount: 4,
        caveAmount: 0,
        floorArea: 4,
        tankId: tankIds[0].id,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.CREATED);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('Decor successfully created');
        chai.expect(res.body.data).to.be.an('array');
        decorId = res.body.data.at(-1).id; //most recent created decor
        done();
      });
  });

  //validate create
  it('should error in create validation', (done) => {
    chai
      .request(app)
      .post('/api/v1/decors')
      .send({
        name: 'Mossy Rocks',
        plantAmount: 20,
        rockAmount: 4,
        caveAmount: 0,
        floorArea: 4,
        tankId: tankIds[0].id,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('plantAmount should be equal to or less than 10');
        done();
      });
  });

  //getAll
  it('should get all Decors', (done) => {
    chai
      .request(app)
      .get('/api/v1/decors')
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('array');
        done();
      });
  });

  //getOne
  it('should get Decor by id', (done) => {
    chai
      .request(app)
      .get(`/api/v1/decors/${decorId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //update
  it('should update decor by id', (done) => {
    chai
      .request(app)
      .put(`/api/v1/decors/${decorId}`)
      .send({
        plantAmount: 2,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Decor with the id: ${decorId} successfully updated`);
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //validate update
  it('should error in update validation', (done) => {
    chai
      .request(app)
      .put(`/api/v1/decors/${decorId}`)
      .send({
        tankId: 'big tank',
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('tankId should be of uuid version 4 format');
        done();
      });
  });

  //delete
  it('should delete decor by id', (done) => {
    chai
      .request(app)
      .delete(`/api/v1/decors/${decorId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Decor with the id: ${decorId} successfully deleted`);
        done();
      });
  });
});
