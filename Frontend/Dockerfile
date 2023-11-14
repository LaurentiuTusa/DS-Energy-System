FROM node:17-alpine

WORKDIR /app

COPY ./energy_project/package.json .

RUN npm install

COPY . .

EXPOSE 3000

CMD ["npm", "start", "--prefix", "energy_project"]
