FROM node:14-alpine as node
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:1.21.6-alpine
COPY --from=node /app/dist/geo-distance-client /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
