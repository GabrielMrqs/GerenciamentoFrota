# Stage 1
FROM node as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

# Stage 2
FROM nginx:alpine
COPY dist/gerenciamento-frota /usr/share/nginx/html