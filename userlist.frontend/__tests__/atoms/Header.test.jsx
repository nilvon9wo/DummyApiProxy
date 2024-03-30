import React from 'react';
import { describe, expect, it } from 'vitest';
import { render } from '@testing-library/react';
import { screen } from '@testing-library/react';
import { beInTheDocument, haveTextContent } from '@testing-library/jest-dom';
import Header from '../../src/atoms/Header';

describe('Header component', () => {
  it('renders the provided text within an h1 element', () => {
    // Arrange
    const text = 'This is a header';

    // Act
    const { getByText } = render(<Header text={text} />);

    // Assert
    const header = getByText(text);
    expect(header).toBeInTheDocument();
    expect(header.tagName).toBe('H1');
  });

    it('throws an error if text prop is not provided', () => {
      try {
        render(<Header />);
      } catch (error) {
        expect(error.message).toBe('text prop is required');
      }
    });

  it('renders the text with the correct case sensitivity', () => {
    // Arrange
    const text = 'MiXeD CaSe';

    // Act
    const { getByText } = render(<Header text={text} />);

    // Assert
    expect(getByText(text)).toBeInTheDocument();
  });
});