package repository

import (
	"context"
	"time"

	"github.com/amieldelatorre/superlist/backend/models"
	"github.com/google/uuid"
)

type MongoRepository struct {
	// db *mongo.Database
	Hello string
}

// func NewRepository(db *mongo.Database) Repository {
// 	return &repository{db: db}
// }

func NewMongoRepository(name string) Repository {
	return &MongoRepository{Hello: name}
}

func (r MongoRepository) GetSuperList(ctx context.Context, id string) (models.SuperList, error) {
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

func (r MongoRepository) CreateSuperList(ctx context.Context, description string, sortOrder int, listItems []models.ListItem) (models.SuperList, error) {
	superList := models.SuperList{
		UUID:        uuid.NewString(),
		DateCreated: time.Now(),
		LastUpdated: time.Now(),
		Description: description,
		SortOrder:   sortOrder,
		ListItems:   listItems,
	}

	return superList, nil
}

func (r MongoRepository) Lists(ctx context.Context) []models.SuperList {
	var lists []models.SuperList
	return lists
}
