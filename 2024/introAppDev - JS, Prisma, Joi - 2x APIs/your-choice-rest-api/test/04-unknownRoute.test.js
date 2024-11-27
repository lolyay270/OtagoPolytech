/**
 * @file Test a route that is not being used
 * @author Jenna Boyes
 */

import chai from 'chai';
import chaiHttp from 'chai-http';
import { describe, it, before } from 'mocha';
import app from '../app.js';
import STATUS_CODES from '../utils/statusCode.js';

chai.use(chaiHttp);

describe('Unknown Route', () => {
  it('Should give 404 error', (done) => {
    chai
      .request(app)
      .get('/api/random_route')
      .end((req, res) => {
        chai.expect(res.status).to.be.equal(STATUS_CODES.NOT_FOUND);
        chai.expect(res.body).to.be.an('object');
        chai.expect(res.body.msg).to.be.equal('Page not found');
        done();
      });
  });
});
