package repository

import (
	"context"
	"time"

	"github.com/amieldelatorre/superlist/backend/models"
	"github.com/google/uuid"
)

type MemoryRepository struct {
	SuperLists []models.SuperList
}

func NewMemoryRepository() Repository {
	return &MemoryRepository{SuperLists: make([]models.SuperList, 0)}
}

func (r MemoryRepository) GetSuperList(ctx context.Context, id string) (models.SuperList, error) {
	retVal := &models.SuperList{}

	for _, list := range r.SuperLists {
		if list.UUID == id {
			retVal = &list
			break
		}
	}

	return *retVal, nil
}

func (r *MemoryRepository) CreateSuperList(ctx context.Context, description string, sortOrder int, listItems []models.ListItem) (models.SuperList, error) {
	superList := models.SuperList{
		UUID:        uuid.NewString(),
		DateCreated: time.Now(),
		LastUpdated: time.Now(),
		Description: description,
		SortOrder:   sortOrder,
		ListItems:   listItems,
	}

	r.SuperLists = append(r.SuperLists, superList)
	return superList, nil
}

func (r MemoryRepository) Lists(ctx context.Context) []models.SuperList {
	return r.SuperLists
}
