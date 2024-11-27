/**
 * @file Core of the API
 * @author Jenna Boyes
 */

//import external tools
import express from 'express';
import swaggerJSDoc from 'swagger-jsdoc';
import swaggerUi from 'swagger-ui-express';
import listEndpoints from 'express-list-endpoints';

//import routes
import levelRoutes from './routes/v1/level.js';
import tankRoutes from './routes/v1/tank.js';
import fishRoutes from './routes/v1/fish.js';
import decorRoutes from './routes/v1/decor.js';
import { getPageNotFound } from './routes/pageNotFound.js';

//import middleware tools
import { isContentTypeApplicationJSON } from './middleware/utils.js';

//create express application
const app = express();

//setup port number for localhost use
const PORT = process.env.PORT || 3000;

//use external tools
app.use(express.urlencoded({ extended: false }));
app.use(express.json());
const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'Megaquarium Game API',
      version: '1.0.0',
      description:
        'API to hold basic info about the fish, tanks and decorations available at the start of a given level',
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
app.use(isContentTypeApplicationJSON);

// use routes
app.use('/api/v1/levels', levelRoutes);
app.use('/api/v1/tanks', tankRoutes);
app.use('/api/v1/fishes', fishRoutes);
app.use('/api/v1/decors', decorRoutes);

//setup swagger UI
app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

//list of endpoints that exist
const getAvailableEndpoints = () => {
  const endpoints = listEndpoints(app);
  const data = ['localhost:3000/api-docs'];
  endpoints.forEach((endpoint) => {
    if (endpoint.path.includes('/ ') || endpoint.path.includes(':id')) return;
    data.push(`localhost:3000${endpoint.path}`);
  });
  return data;
};

//show lists of endpoint on home url
app.get('/', (req, res) => {
  return res.status(200).json(getAvailableEndpoints());
});


//any routes that are not stated above
app.use('/*', getPageNotFound);

// Start the server on port 3000
app.listen(PORT, () => {
  console.log(
    `Server is listening on port ${PORT}. Visit http://localhost:${PORT}`,
  );
});

export default app;
