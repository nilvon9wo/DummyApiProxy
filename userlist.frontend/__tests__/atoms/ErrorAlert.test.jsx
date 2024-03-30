import React from 'react';
import { describe, expect, it } from 'vitest';
import { render } from '@testing-library/react';
import { screen } from '@testing-library/react';
import { beInTheDocument, haveTextContent } from '@testing-library/jest-dom';
import ErrorAlert from '../../src/atoms/ErrorAlert';

describe('ErrorAlert component', () => {
  it('renders nothing when message is empty', () => {
    // Arrange
    const message = '';

    // Act
    const { container } = render(<ErrorAlert message={message} />);

    // Assert
    expect(container.firstChild).toBeNull();
  });

  it('renders the error message in red when message is provided', () => {
    // Arrange
    const message = 'This is an error message';

    // Act
    render(<ErrorAlert message={message} />);

    // Assert
    const errorMessage = screen.getByText(message);
    expect(errorMessage).toBeInTheDocument();
    expect(errorMessage).toHaveTextContent(message);
    expect(window.getComputedStyle(errorMessage).color).toMatch(/rgb\(255, 0, 0\)/);
  });

  it('renders nothing when message is null', () => {
    // Arrange
    const message = null;

    // Act
    const { container } = render(<ErrorAlert message={message} />);

    // Assert
    expect(container.firstChild).toBeNull();
  });

  it('renders nothing when message is undefined', () => {
    // Arrange
    const message = undefined;

    // Act
    const { container } = render(<ErrorAlert message={message} />);

    // Assert
    expect(container.firstChild).toBeNull();
  });
});
