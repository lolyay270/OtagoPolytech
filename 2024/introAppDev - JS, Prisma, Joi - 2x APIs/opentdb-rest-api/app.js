/**
 * @file imports everything the app needs and runs the app on a port
 * @author Jenna Boyes
 */

// Import the external tools
import express from 'express';
import swaggerJSDoc from 'swagger-jsdoc';
import swaggerUi from 'swagger-ui-express';
import { isContentTypeApplicationJSON } from './middleware/utils.js';
import authRouteMiddleware from './middleware/authRoute.js';
import compression from 'compression';
import helmet from 'helmet';
import rateLimit from "express-rate-limit";

// Import the all routes modules
import userRoutes from './routes/v1/user.js';
import quizRoutes from './routes/v1/quiz.js';
import scoresRoutes from './routes/v1/userQuizScore.js';
import authRoutes from './routes/v1/auth.js';

// Create an Express application
const app = express();

// Use the PORT environment variable or 3000
const PORT = process.env.PORT || 3000;

//----Use external tools----\\
app.use(express.urlencoded({ extended: false })); // parse incoming requests, urlencoded payloads. For example, form data
app.use(express.json()); // parse incoming requests, JSON payloads. For example, REST API requests
app.use(compression()); //improve the application's performance by reducing the payload size
app.use(
  //hide what apps this API is using from attackers
  helmet({
    xPoweredBy: true,
  }),
);
//limit amount of requests
const limiter = rateLimit({
  windowMs: 15 * 60 * 1000, // 15 minutes
  max: 100, // limit each IP to 100 requests per windowMs
  message: 'Too many requests from this IP, please try again after 15 minutes',
});
app.use(limiter);
//swagger
const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Intro App Dev API',
      version: '1.0.0',
      description: 'I am learning how to make an API',
      contact: {
        name: 'Jenna Boyes',
      },
    },
    servers: [
      {
        url: 'http://localhost:3000',
      },
    ],
  },
  apis: ['./routes/v1/*.js'],
};
const swaggerDocs = swaggerJSDoc(swaggerOptions);

//use middleware
app.use(isContentTypeApplicationJSON);

// Use the routes modules
app.use('/api/v1/users', authRouteMiddleware, userRoutes);
app.use('/api/v1/quizzes', authRouteMiddleware, quizRoutes);
app.use('/api/v1/scores', authRouteMiddleware, scoresRoutes);
app.use('/api/v1/auth', authRoutes);
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

// Start the server on port 3000
app.listen(PORT, () => {
  console.log(
    `Server is listening on port ${PORT}. Visit http://localhost:${PORT}`,
  );
});

// Export the Express application. May be used by other modules. For example, API testing
export default app;
