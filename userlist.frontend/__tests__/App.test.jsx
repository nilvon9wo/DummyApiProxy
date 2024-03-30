import React from 'react';
import { describe, expect, it, beforeEach, afterEach } from 'vitest'; // Adjust for your testing framework
import { render, screen } from '@testing-library/react';
import { beInTheDocument, haveTextContent } from '@testing-library/jest-dom';
import App from '../src/App';
import UserList from '../src/features/users/UserList';
import { Response } from 'node-fetch';
import nock from 'nock';


describe('App component', () => {
  beforeEach(() => {
    const mockUsers = [{ id: 1, firstName: 'John', lastName: 'Doe' }];
    global.fetch = vi.fn(() => Promise.resolve(new Response(JSON.stringify({ users: mockUsers }))));
  });

  afterEach(() => {
    nock.cleanAll();
  });

  it('renders the UserList component', () => {
    // Arrange (nothing needed in this case)

    // Act
    render(<App />);

    // Assert
    expect(screen.getByRole('list')).toBeInTheDocument();

    // Optional: Test specific UserList props (if applicable)
    // ...
  });
});