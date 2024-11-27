/**
 * @file Test functions for Tanks
 * @author Jenna Boyes
 */

import chai from 'chai';
import chaiHttp from 'chai-http';
import { describe, it, before } from 'mocha';
import app from '../app.js';
import STATUS_CODES from '../utils/statusCode.js';
import { prismaClient } from '../prisma/prisma.js';

chai.use(chaiHttp);

const levelIds = await prismaClient.level.findMany({ select: { id: true } }); //items 0 and 1 are seeded
let tankId;

describe('Tanks', () => {
  //create
  it('should create Tank', (done) => {
    chai
      .request(app)
      .post('/api/v1/tanks')
      .send({
        name: 'Tunnel Tank',
        totalFishSize: 35,
        floorArea: 15,
        rounded: true,
        levels: [levelIds[0].id],
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.CREATED);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('Tank successfully created');
        chai.expect(res.body.data).to.be.an('array');
        tankId = res.body.data.at(-1).id; //most recent created tank
        done();
      });
  });

  //validate create
  it('should error in create validation', (done) => {
    chai
      .request(app)
      .post('/api/v1/tanks')
      .send({
        name: 'Tunnel Tank',
        totalFishSize: 35,
        floorArea: 15,
        rounded: 'yes', //bool
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('rounded should be true or false');
        done();
      });
  });

  //getAll
  it('should get all Tanks', (done) => {
    chai
      .request(app)
      .get('/api/v1/tanks')
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('array');
        done();
      });
  });

  //getOne
  it('should get Tank by id', (done) => {
    chai
      .request(app)
      .get(`/api/v1/tanks/${tankId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //update
  it('should update tank by id', (done) => {
    chai
      .request(app)
      .put(`/api/v1/tanks/${tankId}`)
      .send({
        name: 'Jelly Safe Tank',
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Tank with the id: ${tankId} successfully updated`);
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //validate update
  it('should error in update validation', (done) => {
    chai
      .request(app)
      .put(`/api/v1/tanks/${tankId}`)
      .send({
        floorArea: -3,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('floorArea should be equal to or greater than 1');
        done();
      });
  });

  //delete
  it('should delete tank by id', (done) => {
    chai
      .request(app)
      .delete(`/api/v1/tanks/${tankId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Tank with the id: ${tankId} successfully deleted`);
        done();
      });
  });
});
