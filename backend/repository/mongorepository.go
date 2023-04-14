package repository

import (
	"context"
	"time"

	"github.com/amieldelatorre/superlist/backend/models"
	"github.com/google/uuid"
)

type repository struct {
	// db *mongo.Database
	Hello string
}

// func NewRepository(db *mongo.Database) Repository {
// 	return &repository{db: db}
// }

func NewRepository(name string) Repository {
	return &repository{Hello: name}
}

func (r repository) GetSuperList(ctx context.Context, id string) (models.SuperList, error) {
	var list models.SuperList

	list = models.SuperList{
		UUID:        uuid.NewString(),
		DateCreated: time.Now(),
		LastUpdated: time.Now(),
		Description: "Nothing important",
		SortOrder:   1,
	}

	return list, nil
}
