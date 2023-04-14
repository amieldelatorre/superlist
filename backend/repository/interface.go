package repository

import (
	"context"

	"github.com/amieldelatorre/superlist/backend/models"
)

type Repository interface {
	GetSuperList(ctx context.Context, uuid string) (models.SuperList, error)
	// CreateSuperList(ctx context.Context, in models.SuperList) (models.SuperList, error)
	// UpdateSuperList(ctx context.Context, in models.SuperList) (models.SuperList, error)
	// DeleteSuperList(ctx context.Context, uuid string) error
}
