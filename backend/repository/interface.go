package repository

import (
	"context"

	"github.com/amieldelatorre/superlist/backend/models"
)

type Repository interface {
	GetSuperList(ctx context.Context, uuid string) (models.SuperList, error)
	CreateSuperList(ctx context.Context, description string, sortOrder int, listItems []models.ListItem) (models.SuperList, error)
	Lists(ctx context.Context) []models.SuperList // For testing purposes will delete
	// UpdateSuperList(ctx context.Context, in models.SuperList) (models.SuperList, error)
	// DeleteSuperList(ctx context.Context, uuid string) error
}
