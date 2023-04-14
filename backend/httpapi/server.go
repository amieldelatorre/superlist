package httpapi

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
	"net/http"

	"github.com/amieldelatorre/superlist/backend/models"
	"github.com/amieldelatorre/superlist/backend/repository"
)

type Server struct {
	repository repository.Repository
}

func NewServer(repository repository.Repository) *Server {
	return &Server{repository: repository}
}

func (s Server) SuperListHandler(writer http.ResponseWriter, request *http.Request) {
	writer.Header().Set("Content-Type", "application/json")
	logMessage := fmt.Sprintf("Query on path: %s", request.URL.Path)
	log.Default().Println(logMessage)

	if request.Method == "POST" {
		s.CreateSuperList(writer, request)
		return
	}

	id := request.URL.Path[len("/api/v1/superlist/"):]
	if id == "" {
		writer.WriteHeader(http.StatusBadRequest)
		writer.Write([]byte("Bad Request"))
		return
	}

	switch request.Method {
	case "GET":
		s.GetSuperList(writer, request, id)
	}
}

func (s Server) GetSuperList(writer http.ResponseWriter, request *http.Request, id string) {
	logMessage := fmt.Sprintf("%s request on uuid %s on path %s", request.Method, id, request.URL.Path)
	log.Default().Println(logMessage)

	ctx := context.Background()
	superList, err := s.repository.GetSuperList(ctx, id)
	if err != nil {
		log.Fatalln("Error getting superlist")
	}

	if superList.UUID == "" {
		writer.WriteHeader(http.StatusNotFound)
		writer.Write([]byte("Not Found"))
		return
	}

	err2 := json.NewEncoder(writer).Encode(&superList)
	if err2 != nil {
		log.Fatalln("There was an error encoding the initialised struct", err)
	}

	return
}

func (s Server) CreateSuperList(writer http.ResponseWriter, request *http.Request) {
	logMessage := fmt.Sprintf("%s request on on path %s", request.Method, request.URL.Path)
	log.Default().Println(logMessage)

	decoder := json.NewDecoder(request.Body)
	var requestBody map[string]json.RawMessage

	err := decoder.Decode(&requestBody)
	if err != nil {
		writer.WriteHeader(http.StatusBadRequest)
		writer.Write([]byte("Bad Request. Error decoding JSON body"))
		return
	}

	var description string
	var sortOrder int
	var listItems []models.ListItem

	err = json.Unmarshal(requestBody["Description"], &description)
	err = json.Unmarshal(requestBody["SortOrder"], &sortOrder)
	err = json.Unmarshal(requestBody["ListItems"], &listItems)

	ctx := context.Background()
	superList, err2 := s.repository.CreateSuperList(ctx, description, sortOrder, listItems)

	if err2 != nil {
		writer.WriteHeader(http.StatusInternalServerError)
		writer.Write([]byte("Error creating new superlist."))
		return
	}

	err3 := json.NewEncoder(writer).Encode(&superList)
	if err3 != nil {
		log.Fatalln("There was an error encoding the initialised struct", err3)
	}

	return
}

func (s Server) Check(writer http.ResponseWriter, request *http.Request) {
	ctx := context.Background()
	lists := s.repository.Lists(ctx)
	err := json.NewEncoder(writer).Encode(&lists)
	if err != nil {
		log.Fatalln("There was an error encoding the initialised struct", err)
	}

	return
}
