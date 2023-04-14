package httpapi

import (
	"context"
	"encoding/json"
	"log"
	"net/http"

	"github.com/amieldelatorre/superlist/backend/repository"
)

type Server struct {
	repository repository.Repository
}

func NewServer(repository repository.Repository) *Server {
	return &Server{repository: repository}
}

func (s Server) GetSuperList(writer http.ResponseWriter, request *http.Request) {
	writer.Header().Set("Content-Type", "application/json")
	if request.Method != "GET" {
		writer.WriteHeader(http.StatusBadRequest)
		writer.Write([]byte("Bad Request"))
		return
	}
	ctx := context.Background()
	superList, err := s.repository.GetSuperList(ctx, "string")
	if err != nil {
		log.Fatalln("Error getting superlist")
	}

	err2 := json.NewEncoder(writer).Encode(&superList)
	if err2 != nil {
		log.Fatalln("There was an error encoding the initialised struct", err)
	}

	return

}
