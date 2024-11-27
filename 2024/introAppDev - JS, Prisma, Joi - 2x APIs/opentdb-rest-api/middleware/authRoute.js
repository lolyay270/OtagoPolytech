/**
 * @file Authenicates if users have access to specific Routes
 * @author Jenna Boyes
 */

import jwt from 'jsonwebtoken';

const authRoute = (req, res, next) => {
  try {
    //The authorization request header provides information that authenticates
    //a user agent with a server, allowing access to a protected resource.
    const authHeader = req.headers.authorization;

    //return unauthorised if token not provided
    if (!authHeader || !authHeader.startsWith('Bearer ')) {
      return res.status(403).json({
        msg: 'No token provided',
      });
    }

    // Get the JWT from the bearer token
    const token = authHeader.split(' ')[1];

    //Verify the signed JWT is valid
    const payload = jwt.verify(token, process.env.JWT_SECRET);

    // Set Request's user property to the authenticated user
    req.user = payload;

    // Call the next middleware in the stack
    return next();
  } catch (err) {
    return res.status(403).json({
      msg: 'Not authorized to access this route',
    });
  }
};

export default authRoute;
