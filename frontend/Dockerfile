FROM node:latest as build

WORKDIR /app

COPY . .

RUN npm install

RUN npm run build --prod

FROM nginx:latest

COPY --from=build usr/local/app/dist/client-user-crud-app /usr/share/nginx/html

EXPOSE 81
