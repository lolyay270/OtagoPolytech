/**
 * @file Test functions for Fishes
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
let fishId;

describe('Fishes', () => {
  //create
  it('should create Fish', (done) => {
    chai
      .request(app)
      .post('/api/v1/fishes')
      .send({
        scientificName: 'Pterophyllum scalare',
        commonName: 'Amazonian Angelfish',
        maxSize: 4,
        temperature: 'TROPICAL',
        tankId: tankIds[0].id,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.CREATED);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('Fish successfully created');
        chai.expect(res.body.data).to.be.an('array');
        fishId = res.body.data.at(-1).id; //most recent created fish
        done();
      });
  });

  //validate create
  it('should error in create validation', (done) => {
    chai
      .request(app)
      .post('/api/v1/fishes')
      .send({
        scientificName: 'Pterophyllum scalare',
        commonName: 'Amazonian Angelfish',
        temperature: 'TROPICAL',
        tankId: tankIds[0].id,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('maxSize is required');
        done();
      });
  });

  //getAll
  it('should get all Fishes', (done) => {
    chai
      .request(app)
      .get('/api/v1/fishes')
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('array');
        done();
      });
  });

  //getOne
  it('should get Fish by id', (done) => {
    chai
      .request(app)
      .get(`/api/v1/fishes/${fishId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //update
  it('should update fish by id', (done) => {
    chai
      .request(app)
      .put(`/api/v1/fishes/${fishId}`)
      .send({
        maxSize: 5,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Fish with the id: ${fishId} successfully updated`);
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //validate update
  it('should error in update validation', (done) => {
    chai
      .request(app)
      .put(`/api/v1/fishes/${fishId}`)
      .send({
        maxSize: 3.2,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('maxSize should be a whole number');
        done();
      });
  });

  //delete
  it('should delete fish by id', (done) => {
    chai
      .request(app)
      .delete(`/api/v1/fishes/${fishId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Fish with the id: ${fishId} successfully deleted`);
        done();
      });
  });
});
