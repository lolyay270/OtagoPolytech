/**
 * @file Test functions for Levels
 * @author Jenna Boyes
 */

import chai from 'chai';
import chaiHttp from 'chai-http';
import { describe, it, before } from 'mocha';
import app from '../app.js';
import STATUS_CODES from '../utils/statusCode.js';

chai.use(chaiHttp);

let levelId;

describe('Levels', () => {
  //create
  it('should create Level', (done) => {
    chai
      .request(app)
      .post('/api/v1/levels')
      .send({
        number: 3,
        name: 'Quiet River',
        startFloorArea: 100,
        startMoney: 300,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.CREATED);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('Level successfully created');
        chai.expect(res.body.data).to.be.an('array');
        levelId = res.body.data.at(-1).id; //most recent made level
        done();
      });
  });

  //validate create
  it('should error in create validation', (done) => {
    chai
      .request(app)
      .post('/api/v1/levels')
      .send({
        number: 4,
        name: 'Deep Ocean',
        startFloorArea: 'big', //should be int
        startMoney: 300,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal('startFloorArea should be a number');
        done();
      });
  });

  //getAll
  it('should get all Levels', (done) => {
    chai
      .request(app)
      .get('/api/v1/levels')
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('array');
        done();
      });
  });

  //getOne
  it('should get Level by id', (done) => {
    chai
      .request(app)
      .get(`/api/v1/levels/${levelId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //update
  it('should update level by id', (done) => {
    chai
      .request(app)
      .put(`/api/v1/levels/${levelId}`)
      .send({
        name: 'White Capped Waves',
        startMoney: 450,
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Level with the id: ${levelId} successfully updated`);
        chai.expect(res.body.data).to.be.an('object');
        done();
      });
  });

  //validate update
  it('should error in update validation', (done) => {
    chai
      .request(app)
      .put(`/api/v1/levels/${levelId}`)
      .send({
        name: '',
      })
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.VALIDATE_ERROR);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('name cannot be empty');
        done();
      });
  });

  //delete
  it('should delete level by id', (done) => {
    chai
      .request(app)
      .delete(`/api/v1/levels/${levelId}`)
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.OK);
        chai.expect(res.body).to.be.an('object');
        chai
          .expect(res.body.msg)
          .to.be.equal(`Level with the id: ${levelId} successfully deleted`);
        done();
      });
  });
});
