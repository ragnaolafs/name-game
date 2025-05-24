# create the database
sudo docker run --name namegame -e POSTGRES_USER=root -e POSTGRES_PASSWORD=password -e POSTGRES_DB=namegame -p 5432:5432 -v pgdata:/var/lib/postgresql/data -d postgres

# run it
sudo docker run namegame