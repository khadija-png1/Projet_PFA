import { createClient } from '@supabase/supabase-js'

const supabaseUrl = "https://nsfhzohvhhqnqekhdkar.supabase.co"

const supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im5zZmh6b2h2aGhxbnFla2hka2FyIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Nzc4OTMwOTksImV4cCI6MjA5MzQ2OTA5OX0.fAKbf0KJKkKPh6LYnySA9sbJV1pUxTGyZX4kMCJRZdg"

export const supabase = createClient(
  supabaseUrl,
  supabaseAnonKey
)