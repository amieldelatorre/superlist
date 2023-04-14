package models

import (
	"time"
)

type ListItem struct {
	DateCreated   time.Time
	LastUpdated   time.Time
	Completed     bool
	DateCompleted time.Time
	SortOrder     int
	Description   string
	Comments      []ListItemComment
}
