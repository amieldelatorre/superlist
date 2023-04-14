package httpapi

import (
	"fmt"
	"log"
	"net/http"

	"github.com/amieldelatorre/superlist/backend/repository"
)

func Run() {

	repository := repository.NewRepository("superlists")
	server := NewServer(repository)

	http.HandleFunc("/api/v1/superlist", server.GetSuperList)

	port := ":8080"

	logMessage := fmt.Sprintf("Serving and listening on port %s.", port)
	log.Default().Println(logMessage)
	log.Default().Println("Press ctrl + c to exit.")

	err := http.ListenAndServe(port, nil)
	if err != nil {
		log.Fatalln("There was an error in the server,", err)
	}
}

// func handleHello(writer http.ResponseWriter, request *http.Request) {
// 	writer.Header().Set("Content-Type", "application/json")

// 	if request.Method != "GET" {
// 		writer.WriteHeader(http.StatusBadRequest)
// 		writer.Write([]byte("Bad Request"))
// 		return
// 	}

// 	per := models.ListItemComment{
// 		DateCreated: time.Now(),
// 		Author:      "Me",
// 		Comment:     "WOw",
// 	}

// 	err := json.NewEncoder(writer).Encode(&per)
// 	if err != nil {
// 		log.Fatalln("There was an error encoding the initialised struct", err)
// 	}
// 	return
// }
